<template>
    <section class="asset-transfer-popup-detail modal">
        <section class="wrapper-popup w-100 h-100">
            <MISAPopup :heading="headingPopup" @close="$emit('close')" @submit="submit">
                <section class="body w-100 h-100 px-20 pb-14 flex flex-col">
                    <!--  -->
                    <section class="h-50 fw-500 flex item-center fs-14" style="flex: 0 0 50px">
                        {{ general_information }}
                    </section>

                    <!--  -->
                    <section class="general px-20 py-20 br-4">
                        <section class="wrapper grid col-gap-20">
                            <MISAInput
                                :label="transfer_asset_code"
                                required
                                :type="InputType.CODE"
                                v-model="transferAsset.TransferAssetCode"
                            />
                            <MISADatePicker
                                :label="transaction_date"
                                v-model="transferAsset.TransactionDate"
                            />
                            <MISADatePicker
                                :label="transfer_date"
                                v-model="transferAsset.TransferDate"
                            />
                            <MISAInput
                                :label="note"
                                v-model="transferAsset.Note"
                                :placeholder="note"
                                placeholder-italic
                            />
                        </section>
                        <section class="flex item-center h-36 col-gap-40">
                            <section class="flex item-center col-gap-10">
                                <MISAChecbox
                                    :checked="receivers.length > 0 || isChooseReceiveDepartment"
                                    @on-checked="(value) => (isChooseReceiveDepartment = value)"
                                />
                                <h3 class="fw-500">
                                    {{ choose_receive_department }}
                                </h3>
                            </section>

                            <section
                                class="flex item-center col-gap-10"
                                v-if="isChooseReceiveDepartment"
                            >
                                <MISAChecbox />
                                <h3 class="fw-500">
                                    {{ add_receive_department_before }}
                                </h3>
                            </section>
                        </section>
                        <DepartmentReceiver
                            v-if="isChooseReceiveDepartment"
                            v-model:receivers="receivers"
                            v-model:receiverFlags="receiverFlags"
                            :popup-detail-mode="props.popupDetailMode"
                            :transfer-asset-id="transferAsset.TransferAssetId"
                        />
                    </section>

                    <!--  -->
                    <section class="h-50 flex item-center justify-between" style="flex: 0 0 50px">
                        <section
                            class="flex item-center col-gap-20"
                            v-if="listRowIdSelected.length == 0"
                        >
                            <h3 class="fw-500 fs-14">{{ fixed_asset_transfer_information }}</h3>
                            <MISAInput
                                :placeholder="search_fixed_asset"
                                placeholder-italic
                                width="200px"
                            >
                                <template #icon-left>
                                    <section class="icon search"></section>
                                </template>
                            </MISAInput>
                        </section>
                        <MISASelectInfo
                            v-else
                            :selected-total="listRowIdSelected.length"
                            @unselected="listRowIdSelected = []"
                            @delete="deleteManyTransferAsset"
                        />

                        <MISAButton
                            :title="choose_fixed_asset"
                            type-button="sub"
                            padding="0 20px 0 5px"
                            @click="isShowPopupChoose = true"
                        >
                            <template #icon-left>
                                <section class="icon add"></section>
                            </template>
                        </MISAButton>
                    </section>

                    <!--  -->
                    <section class="detail flex br-4">
                        <MISATable
                            border
                            id="FixedAssetId"
                            :data="transferAssetDetailPaging.TransferAssetDetails"
                            :total="transferAssetDetailPaging.TransferAssetDetailTotal"
                            :loading="loadingTable"
                            v-model:pageLimit="transferAssetDetailFilter.PageLimit"
                            v-model:pageNumber="transferAssetDetailFilter.PageNumber"
                            v-model:listRowIdSelected="listRowIdSelected"
                        >
                            <MISATableColumn type="selection" fixed side-fixed="left" />
                            <MISATableColumn type="index" />
                            <MISATableColumn
                                :label="fixed_asset_code"
                                column-key="FixedAssetCode"
                                text-align="left"
                                width="150px"
                            />
                            <MISATableColumn
                                :label="fixed_asset_name"
                                column-key="FixedAssetName"
                                text-align="left"
                                width="250px"
                            />
                            <MISATableColumn
                                :label="cost"
                                column-key="Cost"
                                text-align="right"
                                width="200px"
                                has-total
                                has-format-number
                            />
                            <MISATableColumn
                                :label="remainder_cost"
                                column-key="RemainderCost"
                                text-align="right"
                                width="200px"
                                has-total
                                has-format-number
                            />
                            <MISATableColumn
                                :label="department_name"
                                column-key="DepartmentName"
                                text-align="left"
                                width="250px"
                            />
                            <MISATableColumn
                                :label="transfer_department_name"
                                type="expand"
                                width="250px"
                                column-key="TransferDepartmentName"
                                text-align="left"
                            >
                                <template
                                    #[item]
                                    v-for="(item, index) in fixedAssetIdPaging"
                                    :key="item"
                                >
                                    <MISACombobox
                                        width="192.5px"
                                        v-if="
                                            transferAssetDetailPaging.TransferAssetDetails[index] !=
                                            undefined
                                        "
                                        :list="
                                            transferDepartments.map(
                                                (transferDepartment) =>
                                                    transferDepartment.DepartmentName
                                            )
                                        "
                                        v-model="
                                            transferAssetDetailPaging.TransferAssetDetails[index]
                                                .TransferDepartmentName
                                        "
                                        @select="selectCombobox($event, index)"
                                    />
                                </template>
                            </MISATableColumn>
                            <MISATableColumn
                                :label="reason"
                                type="expand"
                                text-align="left"
                                width="250px"
                            >
                                <template
                                    #[item]
                                    v-for="(item, index) in fixedAssetIdPaging"
                                    :key="item"
                                >
                                    <MISAInput
                                        v-model="
                                            transferAssetDetailPaging.TransferAssetDetails[index]
                                                .Reason
                                        "
                                        v-if="
                                            transferAssetDetailPaging.TransferAssetDetails[index] !=
                                            undefined
                                        "
                                    />
                                </template>
                            </MISATableColumn>
                            <MISATableColumn
                                :label="functional"
                                type="expand"
                                width="100px"
                                fixed
                                side-fixed="right"
                                scroll
                            >
                                <template #[item] v-for="item in fixedAssetIdPaging" :key="item">
                                    <MISAButton
                                        type-button="icon"
                                        circle
                                        width="30px"
                                        height="30px"
                                        :border="false"
                                        @click="deleteTranferAssetDetail(item)"
                                    >
                                        <template #icon>
                                            <section class="icon delete"></section>
                                        </template>
                                    </MISAButton>
                                </template>
                            </MISATableColumn>
                        </MISATable>
                    </section>
                </section>
            </MISAPopup>
        </section>

        <Transition name="fade">
            <PopupChoose
                v-if="isShowPopupChoose"
                @close="isShowPopupChoose = false"
                :transfer-asset-id="transferAsset.TransferAssetId"
                :fixed-asset-id-ignores="fixedAssetIdIgnores"
                :transfer-asset-detail-id-ignores="transferAssetDetailIdIgnores"
                @submit="addTransferAssetDetails"
            />
        </Transition>

        <MISALoading :loading="loading" />

        <MISAToastMessage v-model="showWarningMessage" @click-main="showWarningMessage = false">
            <p class="pr-20">{{ warningMessage }}</p>
        </MISAToastMessage>
    </section>
</template>

<script setup lang="ts">
import { useResource } from '@/hook'
import DepartmentReceiver from './DepartmentReceiver.vue'
import PopupChoose from './PopupChoose.vue'
import { computed, ref, onMounted, watch } from 'vue'
import type {
    Department,
    PopupDetailProps,
    Receiver,
    TransferAsset,
    TransferAssetCreateFull,
    TransferAssetDetail,
    TransferAssetUpdateFull,
    TransferAssetDetailFlag,
    ReceiverFlag,
    TransferAssetDetailFilter,
    MyResponse
} from '@/types'
import { ActionMode, InputType, PopupDetailMode } from '@/enum'
import { useDepartment } from '@/stores'
import { receiverAPI, transferAssetAPI, transferAssetDetailAPI } from '@/api'
import { isAxiosError } from 'axios'

/**
 * Định nghĩ Props cho component
 * @author Bùi Huy Tuyền (10-9-2023)
 */
const props = withDefaults(defineProps<PopupDetailProps>(), {
    popupDetailMode: PopupDetailMode.CREATE
})

/**
 * Định nghĩ các emit cho component
 * @author Bùi Huy Tuyền (10-9-2023)
 */
const emits = defineEmits<{
    close: []
    submit: []
}>()

/**
 * Khởi tạo data cho component
 * @author Bùi Huy Tuyền (10-9-2023)
 */
// Danh sách người nhận
const receivers = ref<Receiver[]>([])
const receiverFlags = ref<ReceiverFlag[]>([])

// Danh sách tài sản điều chuyển
const transferAssetDetails = ref<TransferAssetDetail[]>([])

const transferAssetDetailFlags = ref<TransferAssetDetailFlag[]>([])

// Chứng từ điều chuyển
const transferAsset = ref<TransferAsset>({
    TransferAssetId: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
    TransferAssetCode: '',
    TransferDate: new Date(new Date().toISOString()).toISOString(),
    TransactionDate: new Date(new Date().toISOString()).toISOString(),
    Cost: 0,
    RemainderCost: 0,
    Note: ''
})

//
const tranferAssetCreateFull = ref<TransferAssetCreateFull | null>(null)

const tranferAssetUpdateFull = ref<TransferAssetUpdateFull>()

// Biến hiển thị form chọn phòng ban nhận
const isChooseReceiveDepartment = ref<boolean>(false)

// Biến hiển thị form chọn tài sản điều chuyển
const isShowPopupChoose = ref<boolean>(false)

const transferAssetDetailFilter = ref<TransferAssetDetailFilter>({
    PageLimit: 10,
    PageNumber: 1,
    TransferAssetId: props.transferAssetUpdate?.TransferAssetId ?? ''
})

// Danh sách phòng ban điều chuyển
const transferDepartments = ref<readonly Department[]>([])

const listRowIdSelected = ref<string[]>([])

const loadingTable = ref<boolean>(false)

const loading = ref<boolean>(false)

const showWarningMessage = ref<boolean>(false)
const warningMessage = ref<string>('')
/**
 * Định nghĩa các computed cho component
 * @author Bùi Huy Tuyền (10-9-2023)
 */
const transferAssetDetailPaging = computed(() => ({
    TransferAssetDetails: transferAssetDetails.value.slice(
        (transferAssetDetailFilter.value.PageNumber - 1) *
            transferAssetDetailFilter.value.PageLimit,
        transferAssetDetailFilter.value.PageNumber * transferAssetDetailFilter.value.PageLimit
    ),
    TransferAssetDetailTotal: transferAssetDetails.value.length
}))

const headingPopup = computed(() => {
    switch (props.popupDetailMode) {
        case PopupDetailMode.UPDATE:
            return update
        default:
            return create
    }
})

const fixedAssetIdPaging = computed(() =>
    transferAssetDetailPaging.value.TransferAssetDetails.map(
        (transferAssetDetail) => transferAssetDetail.FixedAssetId
    )
)

const fixedAssetIdIgnores = computed(() =>
    transferAssetDetails.value.map((transferAssetDetail) => transferAssetDetail.FixedAssetId)
)

const transferAssetDetailIdIgnores = computed(
    () =>
        transferAssetDetailFlags.value
            .filter(
                (transferAssetDetailFlag) => transferAssetDetailFlag.ActionMode == ActionMode.DELETE
            )
            .map(
                (transferAssetDetailFlag) =>
                    transferAssetDetailFlag.TransferAssetDetail.TransferAssetDetailId
            ) as string[]
)

/**
 *  Lấy danh sách người nhận theo mã chứng từ điều chuyển
 * @param transferAssetId
 * @author Bùi Huy Tuyền (10-9-2023)
 */
const getReceivers = async (transferAssetId: string) => {
    const response = await receiverAPI.getReceivers(transferAssetId)
    receivers.value = JSON.parse(JSON.stringify(response.data))
    if (receivers.value.length > 0) {
        isChooseReceiveDepartment.value = true
    }
}

const getTransferCodeNew = async () => {
    const response = await transferAssetAPI.getTransferAssetCodeNew()
    transferAsset.value.TransferAssetCode = response.data
}

const getTransferAssetDetails = async (transferAssetId: string) => {
    loadingTable.value = true
    const response = await transferAssetDetailAPI.getManyByTransferAssetId(transferAssetId)
    transferAssetDetails.value = response.data
    const timmer = setTimeout(() => {
        loadingTable.value = false
        clearTimeout(timmer)
    }, 500)
}

const selectCombobox = (value: string, index: number) => {
    transferAssetDetailPaging.value.TransferAssetDetails[index].TransferDepartmentId =
        useDepartment().getDepartmentByName(value)?.DepartmentId
    if (props.popupDetailMode == PopupDetailMode.UPDATE) {
        const transferAssetDetailFlag: TransferAssetDetailFlag = {
            ActionMode: 0,
            TransferAssetDetail: transferAssetDetailPaging.value.TransferAssetDetails[index]
        }

        if (
            !transferAssetDetailFlags.value
                .map(
                    (transferAssetDetailFlag) =>
                        transferAssetDetailFlag.TransferAssetDetail.FixedAssetId
                )
                .includes(transferAssetDetailFlag.TransferAssetDetail.FixedAssetId)
        ) {
            console.log(transferAssetDetailFlag)
            transferAssetDetailFlags.value.push(transferAssetDetailFlag)
        }
    }
}

const addTransferAssetDetails = (list: TransferAssetDetail[]) => {
    loadingTable.value = true
    transferAssetDetails.value.unshift(...list)
    console.log(transferAssetDetails.value)

    if (props.popupDetailMode == PopupDetailMode.UPDATE) {
        transferAssetDetailFlags.value.push(
            ...list.map(
                (transferAssetDetail) =>
                    ({
                        TransferAssetDetail: transferAssetDetail,
                        ActionMode: 1
                    }) as TransferAssetDetailFlag
            )
        )
    }
    const timmer = setTimeout(() => {
        loadingTable.value = false
        clearTimeout(timmer)
    }, 500)
}

const deleteManyTransferAsset = () => {
    const transferAssetDeletes = transferAssetDetails.value.filter((transferAssetDetail) =>
        listRowIdSelected.value.includes(transferAssetDetail.FixedAssetId)
    )
    transferAssetDetails.value = transferAssetDetails.value.filter(
        (transferAssetDetail) => !listRowIdSelected.value.includes(transferAssetDetail.FixedAssetId)
    )

    if (props.popupDetailMode == PopupDetailMode.UPDATE) {
        transferAssetDetailFlags.value = transferAssetDetailFlags.value.filter(
            (transferAssetDetailFlag) =>
                !listRowIdSelected.value.includes(
                    transferAssetDetailFlag.TransferAssetDetail.FixedAssetId
                )
        )

        if (transferAssetDeletes.length > 0) {
            const transferAssetDetailFlagsDelete = transferAssetDeletes.map(
                (transferAssetDetail) =>
                    ({
                        TransferAssetDetail: transferAssetDetail,
                        ActionMode: 2
                    }) as TransferAssetDetailFlag
            )
            transferAssetDetailFlags.value.push(...transferAssetDetailFlagsDelete)
        }
    }

    listRowIdSelected.value = []
}

const deleteTranferAssetDetail = (fixedAssetId: string) => {
    const tranferAssetDetailDelete = transferAssetDetails.value.find(
        (transferAssetDetail) => transferAssetDetail.FixedAssetId == fixedAssetId
    )
    transferAssetDetails.value = transferAssetDetails.value.filter(
        (transferAssetDetail) => transferAssetDetail.FixedAssetId != fixedAssetId
    )
    if (props.popupDetailMode == PopupDetailMode.UPDATE) {
        transferAssetDetailFlags.value = transferAssetDetailFlags.value.filter(
            (transferAssetDetailFlag) =>
                transferAssetDetailFlag.TransferAssetDetail.FixedAssetId != fixedAssetId
        )

        if (tranferAssetDetailDelete) {
            const transferAssetDetailFlag: TransferAssetDetailFlag = {
                ActionMode: 2,
                TransferAssetDetail: tranferAssetDetailDelete
            }
            transferAssetDetailFlags.value.push(transferAssetDetailFlag)
            console.log(transferAssetDetailFlags.value)
        }
    }
}

const submit = async () => {
    try {
        loading.value = true
        if (props.popupDetailMode === PopupDetailMode.CREATE) {
            tranferAssetCreateFull.value = {
                TransferAsset: transferAsset.value,
                TransferAssetDetails: transferAssetDetails.value,
                Receivers: receivers.value
            }
            await transferAssetAPI.createTransferAssetFull(tranferAssetCreateFull.value)
        } else if (props.popupDetailMode === PopupDetailMode.UPDATE) {
            // const tranferAssetDetailFlagsDelete = transferAssetDetailFlags.value
            //     .filter((transferAssetDetailFlag) => transferAssetDetailFlag.ActionMode == 2)
            //     .map((transferAssetDetailFlag) => transferAssetDetailFlag.TransferAssetDetail)

            // const cost = tranferAssetDetailFlagsDelete.reduce(
            //     (total, transferAssetDetail) => total + Number(transferAssetDetail.Cost),
            //     0
            // )
            // const remainderCost = tranferAssetDetailFlagsDelete.reduce(
            //     (total, transferAssetDetail) =>
            //         total + Number(transferAssetDetail.RemainderCost),
            //     0
            // )

            // transferAsset.value.Cost = Number(transferAsset.value.Cost) - cost
            // transferAsset.value.RemainderCost =
            //     Number(transferAsset.value.RemainderCost) - remainderCost
            const receiverFlagUpdate = receivers.value
                .filter((receiver) => receiver.ReceiverId && receiver.TransferAssetId)
                .map(
                    (receiver) =>
                        ({
                            Receiver: receiver,
                            ActionMode: 0
                        }) as ReceiverFlag
                )

            const receiverFlagCreate = receivers.value
                .filter((receiver) => !receiver.ReceiverId && !receiver.TransferAssetId)
                .map(
                    (receiver) =>
                        ({
                            Receiver: receiver,
                            ActionMode: 1
                        }) as ReceiverFlag
                )

            receiverFlags.value.push(...receiverFlagCreate, ...receiverFlagUpdate)
            tranferAssetUpdateFull.value = {
                TransferAsset: transferAsset.value,
                TransferAssetDetails: transferAssetDetailFlags.value,
                Receivers: receiverFlags.value
            }
            await transferAssetAPI.updateTransferAssetFull(tranferAssetUpdateFull.value)
        }
        emits('submit')
        emits('close')
    } catch (error) {
        if (isAxiosError<MyResponse>(error)) {
            showWarningMessage.value = true
            warningMessage.value = error.response?.data.UserMessage ?? ''
        }
    } finally {
        const timmer = setTimeout(() => {
            loading.value = false
            clearTimeout(timmer)
        }, 1000)
    }
}

// Lấy danh sách phòng ban điều chuyển
watch(
    useDepartment().departments,
    () => {
        transferDepartments.value = useDepartment().departments
    },
    { immediate: true }
)

watch(
    [transferAssetDetails, transferAssetDetails.value, () => transferAssetDetails.value.length],
    () => {
        transferAsset.value.Cost = transferAssetDetails.value.reduce(
            (total, transferAssetDetail) => total + Number(transferAssetDetail.Cost),
            0
        )
        transferAsset.value.RemainderCost = transferAssetDetails.value.reduce(
            (total, transferAssetDetail) => total + Number(transferAssetDetail.RemainderCost),
            0
        )
        console.log(transferAsset.value)
    }
)

onMounted(() => {
    switch (props.popupDetailMode) {
        case PopupDetailMode.UPDATE:
            if (!props.transferAssetUpdate) return
            transferAsset.value = props.transferAssetUpdate
            getReceivers(props.transferAssetUpdate.TransferAssetId)
            getTransferAssetDetails(props.transferAssetUpdate.TransferAssetId)
            break
        default:
            getTransferCodeNew()
            break
    }
})

const {
    asset_transfer_popup_detail: {
        general_information,
        transfer_asset_code,
        transaction_date,
        transfer_date,
        note,
        choose_receive_department,
        search_fixed_asset,
        add_receive_department_before,
        cost,
        department_name,
        fixed_asset_code,
        fixed_asset_name,
        fixed_asset_transfer_information,
        functional,
        remainder_cost,
        reason,
        transfer_department_name,
        choose_fixed_asset,
        heading: { create, update }
    }
} = useResource()
</script>

<style scoped lang="scss">
@import './PopupDetail.scss';
</style>
