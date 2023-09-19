<template>
    <section class="transfer-asset-info w-100 h-100 flex flex-col row-gap-5">
        <p v-html="message" class="transfer-asset-info__message"></p>

        <a @click="showMoreInfo = !showMoreInfo">
            {{ showMoreInfo ? hidden_more_info : show_more_info }}
        </a>

        <ul v-if="showMoreInfo" class="transfer-asset-info__more-info row-gap-5 flex flex-col">
            <li v-html="info" v-for="info in moreInfo" :key="info"></li>
        </ul>
    </section>
</template>

<script setup lang="ts">
import type { TransferAssetInfoProps } from '@/types'
import { ref } from 'vue'
import { useResource } from '@/hook'

withDefaults(defineProps<TransferAssetInfoProps>(), { message: '', moreInfo: () => [] })

const showMoreInfo = ref<boolean>(false)

const {
    transfer_asset_info: { show_more_info, hidden_more_info }
} = useResource()
</script>

<style scoped lang="scss">
@import './TransferAssetInfo.scss';
</style>
