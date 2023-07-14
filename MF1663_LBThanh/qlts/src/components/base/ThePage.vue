<template>
    <div class="page">
        <PageHeader></PageHeader>
        <PageContent></PageContent>
    </div>
</template>

<script>
import PageHeader from './PageHeader.vue';
import PageContent from './PageContent.vue'
export default {
  name: 'ThePage',
  components: {
    PageHeader, PageContent
  }
}
</script>