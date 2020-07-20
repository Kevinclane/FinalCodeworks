<template>
  <div class="col-3 my-2">
    <router-link :to="{ name: 'keepDetails', params: { keepId: keep.id } }">
      <img :src="keep.img" alt="error loading image" class="keepImg" />
    </router-link>
  </div>
</template>

<script>
export default {
  name: "KeepComponent",
  props: ["keep"]
};
</script>
<style scoped>
.keepImg {
  width: 100%;
  box-shadow: 2px 4px black !important;
  border-radius: 10px;
}
</style>