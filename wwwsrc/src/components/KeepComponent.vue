<template>
  <div class="col-4 my-2 d-inline-block w-30">
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
.w-30 {
  width: 30%;
}
</style>