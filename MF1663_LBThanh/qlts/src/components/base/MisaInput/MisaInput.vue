<template>
  <input
    :type="type"
    :placeholder="placeholder"
    :value="value"
    @input="$emit('input', $event.target.value)"
  />
</template>

<script>
export default {
  props: {
    type: {
      type: String,
      default: "text",
    },
    placeholder: {
      type: String,
      default: "",
    },
    value: {
      type: String,
      default: "",
    },
  },
};
</script>
