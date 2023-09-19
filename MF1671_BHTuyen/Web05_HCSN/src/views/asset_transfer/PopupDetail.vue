<template>
    <section class="asset-transfer-popup-detail modal">
        <section class="wrapper-popup w-100 h-100">
            <MISAPopup
                :heading="headingPopup"
                @close="closePopupDetail"
                @submit="submitPopupDetail"
                ref="popup"
            >
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
                                :max-length="20"
                                required
                                :type="InputType.CODE"
                                v-model="transferAsset.TransferAssetCode"
                                ref="transferAssetCodeInput"
                            />
                            <MISADatePicker
                                :label="transaction_date"
                                v-model="transferAsset.TransactionDate"
                                ref="transactionDateInput"
                            />
                            <MISADatePicker
                                :label="transfer_date"
                                v-model="transferAsset.TransferDate"
                                ref="transferDateInput"
                            />
                            <MISAInput
                                :label="note"
                                :max-length="100"
                                v-model="transferAsset.Note"
                                :placeholder="note"
                                placeholder-italic
                            />
                        </section>
                        <section class="flex item-center h-36 col-gap-40">
                            <section class="flex item-center col-gap-10">
                                <MISAChecbox
                                    :checked="isChooseReceiveDepartment"
                                    @on-checked="isChooseReceiveDepartment = $event"
                                />
                                <h3
                                    class="fw-500 pointer"
                                    @click="isChooseReceiveDepartment = !isChooseReceiveDepartment"
                                >
                                    {{ choose_receive_department }}
                                </h3>
                            </section>

                            <section
                                class="flex item-center col-gap-10"
                                v-if="isChooseReceiveDepartment"
                            >
                                <MISAChecbox
                                    :checked="isChooseReceiveDepartmentRecent"
                                    @on-checked="isChooseReceiveDepartmentRecent = $event"
                                />
                                <h3
                                    class="fw-500 pointer"
                                    @click="
                                        isChooseReceiveDepartmentRecent =
                                            !isChooseReceiveDepartmentRecent
                                    "
                                >
                                    {{ add_receive_department_recent }}
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
                                v-model="searchTransferAssetDetail"
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
                            ref="buttonChooseFixedAsset"
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
                            :empty-data-title="list_detail_empty"
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
                                        :list="departmentNameList"
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

        <MISAToastMessage v-model="showWarningMessageValidate" @click-main="confirmValidate">
            <p class="pr-20" v-html="warningMessageValidate"></p>
        </MISAToastMessage>

        <!-- Thông báo hủy khai báo -->
        <MISAToastMessage
            v-model="showWarningMessageCancel"
            @click-main="submitPopupDetail"
            @click-sub="confirmCancelToast"
            @click-outline="showWarningMessageCancel = false"
            sub-button="Không lưu"
            main-button="Lưu"
            outline-button="Hủy bỏ"
        >
            <p class="pr-20">{{ warningMessageCancel }}</p>
        </MISAToastMessage>
    </section>
</template>

<script setup lang="ts">
import { useDeepObjectEqual, useResource } from '@/hook'
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
    MyResponse,
    PopupDetailMode
} from '@/types'
import { ActionMode, InputType, PopupDetailMode as PopupDetailModeEnum } from '@/enum'
import { departmentAPI, receiverAPI, transferAssetAPI, transferAssetDetailAPI } from '@/api'
import { isAxiosError } from 'axios'
import type MISAInput from '@/components/input'
import type MISADatePicker from '@/components/date_picker'
import type MISAButton from '@/components/button'
import type MISAPopup from '@/components/popup'

/**
 * Định nghĩ Props cho component
 * @author Bùi Huy Tuyền (10-9-2023)
 */
const props = withDefaults(defineProps<PopupDetailProps>(), {
    popupDetailMode: PopupDetailModeEnum.CREATE
})

/**
 * Định nghĩ các emit cho component
 * @author Bùi Huy Tuyền (10-9-2023)
 */
const emits = defineEmits<{
    close: []
    submit: [popupDetailMode: PopupDetailMode]
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

const tranferAssetBeginFull = ref<{
    TransferAsset: TransferAsset
    TransferAssetDetails: TransferAssetDetail[]
    Receivers: Receiver[]
}>()

// Chứng từ điều chuyển
const transferAsset = ref<TransferAsset>({
    TransferAssetId: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
    TransferAssetCode: '',
    TransferDate: new Date().toLocaleString(),
    TransactionDate: new Date().toLocaleString(),
    Cost: 0,
    RemainderCost: 0,
    Note: ''
})

//
const tranferAssetCreateFull = ref<TransferAssetCreateFull | null>(null)

const tranferAssetUpdateFull = ref<TransferAssetUpdateFull>()

// Biến hiển thị form chọn phòng ban nhận
const isChooseReceiveDepartment = ref<boolean>(receivers.value.length > 0)

const isChooseReceiveDepartmentRecent = ref<boolean>(false)

// Biến hiển thị form chọn tài sản điều chuyển
const isShowPopupChoose = ref<boolean>(false)

const transferAssetDetailFilter = ref<TransferAssetDetailFilter>({
    PageLimit: 10,
    PageNumber: 1,
    TransferAssetId: props.transferAssetUpdate?.TransferAssetId ?? ''
})

const listRowIdSelected = ref<string[]>([])

const loadingTable = ref<boolean>(false)

const loading = ref<boolean>(false)

const showWarningMessageValidate = ref<boolean>(false)
const warningMessageValidate = ref<string>('')

const showWarningMessageCancel = ref<boolean>(false)
const warningMessageCancel = ref<string>('')

const departmentList = ref<Department[]>([])
const departmentNameList = computed(() => departmentList.value.map((item) => item.DepartmentName))

const searchTransferAssetDetail = ref<string>('')

const transferAssetCodeInput = ref<InstanceType<typeof MISAInput> | null>(null)
const transactionDateInput = ref<InstanceType<typeof MISADatePicker> | null>(null)
const transferDateInput = ref<InstanceType<typeof MISADatePicker> | null>(null)
const buttonChooseFixedAsset = ref<InstanceType<typeof MISAButton> | null>(null)
const popup = ref<InstanceType<typeof MISAPopup> | null>(null)

const transferAssetDetailResult = computed(() => {
    if (searchTransferAssetDetail.value == '') return transferAssetDetails.value
    return transferAssetDetails.value.filter((transferAssetDetail) =>
        transferAssetDetail.FixedAssetCode.toLowerCase().includes(
            searchTransferAssetDetail.value.toLowerCase()
        )
    )
})
/**
 * Định nghĩa các computed cho component
 * @author Bùi Huy Tuyền (10-9-2023)
 */
const transferAssetDetailPaging = computed(() => ({
    TransferAssetDetails: transferAssetDetailResult.value.slice(
        (transferAssetDetailFilter.value.PageNumber - 1) *
            transferAssetDetailFilter.value.PageLimit,
        transferAssetDetailFilter.value.PageNumber * transferAssetDetailFilter.value.PageLimit
    ),
    TransferAssetDetailTotal: transferAssetDetails.value.length
}))

const headingPopup = computed(() => {
    switch (props.popupDetailMode) {
        case PopupDetailModeEnum.UPDATE:
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

const getDepartment = async () => {
    const response = await departmentAPI.getAllDepartment()
    departmentList.value = response.data
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

const addTransferAssetDetails = (list: TransferAssetDetail[]) => {
    loadingTable.value = true
    transferAssetDetails.value.unshift(...list)
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

    if (props.popupDetailMode == PopupDetailModeEnum.UPDATE) {
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
    if (props.popupDetailMode == PopupDetailModeEnum.UPDATE) {
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
        }
    }
}

const confirmCancelToast = () => {
    showWarningMessageCancel.value = false
    emits('close')
}

const selectCombobox = (value: string, index: number) => {
    transferAssetDetailPaging.value.TransferAssetDetails[index].TransferDepartmentId =
        departmentList.value.find((department) => department.DepartmentName == value)
            ?.DepartmentId ?? ''
}

const validate = (): boolean => {
    if (!transferAsset.value.TransferAssetCode) {
        showWarningMessageValidate.value = true
        warningMessageValidate.value = 'Mã chứng từ điều chuyển không được để trống.'
        return false
    }
    if (!transferAsset.value.TransactionDate) {
        showWarningMessageValidate.value = true
        warningMessageValidate.value = 'Ngày chứng từ không được để trống.'
        return false
    }
    if (!transferAsset.value.TransferDate) {
        showWarningMessageValidate.value = true
        warningMessageValidate.value = 'Ngày điều chuyển không được để trống.'
        return false
    }

    if (
        new Date(transferAsset.value.TransactionDate) > new Date(transferAsset.value.TransferDate)
    ) {
        showWarningMessageValidate.value = true
        warningMessageValidate.value = 'Ngày điều chuyển không được nhỏ hơn ngày chứng từ.'
        return false
    }

    if (transferAssetDetails.value.length == 0) {
        showWarningMessageValidate.value = true
        warningMessageValidate.value = 'Bạn chưa thêm tài sản điều chuyển.'
        return false
    }

    console.log(transferAssetDetails.value)

    transferAssetDetails.value.forEach((transferAssetDetail) => {
        if (!transferAssetDetail.TransferDepartmentName) {
            showWarningMessageValidate.value = true
            warningMessageValidate.value = 'Bạn chưa chọn phòng ban nhận.'
            return false
        }
    })

    return true
}

const confirmValidate = () => {
    showWarningMessageValidate.value = false
    if (!transferAsset.value.TransferAssetCode) {
        transferAssetCodeInput.value?.focus()
        return
    }
    if (!transferAsset.value.TransactionDate) {
        transactionDateInput.value?.focus()
        return
    }
    if (!transferAsset.value.TransferDate) {
        transferDateInput.value?.focus()
        return
    }

    if (
        new Date(transferAsset.value.TransactionDate) > new Date(transferAsset.value.TransferDate)
    ) {
        console.log(new Date(transferAsset.value.TransactionDate))
        console.log(new Date(transferAsset.value.TransferDate))
        transferDateInput.value?.focus()
        return
    }

    if (transferAssetDetails.value.length == 0) {
        buttonChooseFixedAsset.value?.focus()
        return
    }
}

const closePopupDetail = () => {
    if (props.popupDetailMode == PopupDetailModeEnum.CREATE) {
        showWarningMessageCancel.value = true
        warningMessageCancel.value = 'Bạn có muốn hủy khai báo chứng từ điều chuyển này không?'
    } else if (props.popupDetailMode == PopupDetailModeEnum.UPDATE) {
        showWarningMessageCancel.value = true
        if (chechUpdate()) {
            warningMessageCancel.value = 'Bạn có muốn hủy cập nhật chứng từ điều chuyển này không?'
        } else {
            warningMessageCancel.value =
                'Thay đổi chưa được lưu bạn có muốn hủy bỏ thay đổi này không?'
        }
    }
}

const chechUpdate = (): boolean => {
    var transferAssetCopy = JSON.parse(JSON.stringify(transferAsset.value)) as TransferAsset
    transferAssetCopy.TransferDate = new Date(transferAssetCopy.TransferDate).toLocaleDateString()

    transferAssetCopy.TransactionDate = new Date(
        transferAssetCopy.TransactionDate
    ).toLocaleDateString()

    return useDeepObjectEqual(
        tranferAssetBeginFull.value as Object,
        {
            TransferAsset: transferAssetCopy,
            TransferAssetDetails: transferAssetDetails.value,
            Receivers: receivers.value
        } as Object
    )
}

const submitPopupDetail = async () => {
    if (validate()) {
        try {
            loading.value = true
            if (props.popupDetailMode === PopupDetailModeEnum.CREATE) {
                tranferAssetCreateFull.value = {
                    TransferAsset: transferAsset.value,
                    TransferAssetDetails: transferAssetDetails.value,
                    Receivers: receivers.value
                }
                console.log(transferAsset.value)
                await transferAssetAPI.createTransferAssetFull(tranferAssetCreateFull.value)
            } else if (props.popupDetailMode === PopupDetailModeEnum.UPDATE) {
                loading.value = true
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

                const TransferAssetDetailFlagUpdate = transferAssetDetails.value
                    .filter(
                        (transferAssetDetail) =>
                            transferAssetDetail.TransferAssetDetailId &&
                            transferAssetDetail.TransferAssetId
                    )
                    .map(
                        (transferAssetDetail) =>
                            ({
                                TransferAssetDetail: transferAssetDetail,
                                ActionMode: 0
                            }) as TransferAssetDetailFlag
                    )
                const TransferAssetDetailFlagCreate = transferAssetDetails.value
                    .filter(
                        (transferAssetDetail) =>
                            !transferAssetDetail.TransferAssetDetailId &&
                            !transferAssetDetail.TransferAssetId
                    )
                    .map(
                        (transferAssetDetail) =>
                            ({
                                TransferAssetDetail: transferAssetDetail,
                                ActionMode: 1
                            }) as TransferAssetDetailFlag
                    )

                receiverFlags.value.push(...receiverFlagCreate, ...receiverFlagUpdate)

                transferAssetDetailFlags.value.push(
                    ...TransferAssetDetailFlagUpdate,
                    ...TransferAssetDetailFlagCreate
                )

                tranferAssetUpdateFull.value = {
                    TransferAsset: transferAsset.value,
                    TransferAssetDetails: transferAssetDetailFlags.value,
                    Receivers: receiverFlags.value
                }

                await transferAssetAPI.updateTransferAssetFull(tranferAssetUpdateFull.value)
                transferAssetDetailFlags.value = []
                receiverFlags.value = []
            }
            emits('submit', props.popupDetailMode)
            emits('close')
        } catch (error) {
            if (isAxiosError<MyResponse>(error)) {
                showWarningMessageValidate.value = true
                warningMessageValidate.value = error.response?.data.UserMessage ?? ''
            }
        } finally {
            const timmer = setTimeout(() => {
                loading.value = false
                clearTimeout(timmer)
            }, 1000)
        }
    }
}

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
    }
)

onMounted(async () => {
    await getDepartment()
    switch (props.popupDetailMode) {
        case PopupDetailModeEnum.UPDATE:
            if (!props.transferAssetUpdate) return
            transferAsset.value = props.transferAssetUpdate
            await getReceivers(props.transferAssetUpdate.TransferAssetId)
            await getTransferAssetDetails(props.transferAssetUpdate.TransferAssetId)

            var transferAssetCopy = JSON.parse(
                JSON.stringify(props.transferAssetUpdate)
            ) as TransferAsset

            transferAssetCopy.TransferDate = new Date(
                transferAssetCopy.TransferDate
            ).toLocaleDateString()

            transferAssetCopy.TransactionDate = new Date(
                transferAssetCopy.TransactionDate
            ).toLocaleDateString()

            tranferAssetBeginFull.value = JSON.parse(
                JSON.stringify({
                    TransferAsset: transferAssetCopy,
                    TransferAssetDetails: transferAssetDetails.value,
                    Receivers: receivers.value
                })
            )

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
        add_receive_department_recent,
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
        list_detail_empty,
        heading: { create, update }
    }
} = useResource()
</script>

<style scoped lang="scss">
@import './PopupDetail.scss';
</style>
