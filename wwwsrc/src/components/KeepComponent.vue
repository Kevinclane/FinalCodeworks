<template>
  <div class="KeepComponent">
    <div class="card bg-primary cardRoundedCstm">
      <router-link :to="{ name: 'keepDetails', params: { keepId: keep.id } }">
        <img :src="keep.img" alt="error loading image" class="keepImg cardTopRound" />
        <h3 class="text-black ml-3">{{keep.name}}</h3>
        <h4 class="text-black ml-3">{{keep.description}}</h4>
        <div class="col-12 d-flex justify-content-around">
          <p class="text-black">
            <i class="fa fa-eye" aria-hidden="true"></i>
            {{keep.views}}
          </p>
          <p class="text-black">
            <i class="fa fa-key" aria-hidden="true"></i>
            {{keep.keeps}}
          </p>
        </div>
      </router-link>
    </div>
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
}
.cardTopRound {
  border-top-left-radius: 20px;
  border-top-right-radius: 20px;
}
.cardBottomRound {
  box-shadow: 2px 4px black !important;
  border-bottom-left-radius: 20px;
  border-bottom-right-radius: 20px;
}
.cardRoundedCstm {
  border-radius: 20px;
  border: 2px solid black;
}
.w-30 {
  width: 30%;
}
.text-black {
  color: black;
}
a {
  text-decoration: none;
}
</style>