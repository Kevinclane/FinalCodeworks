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
      <div class="col-12 text-center my-2">
        <button @click="deleteKeepFromVault" class="btn btn-danger">Remove from Vault</button>
      </div>
    </div>
  </div>
</template>

<script>
import swal from "sweetalert";
export default {
  name: "KeepComponent",
  props: ["keep"],
  methods: {
    deleteKeepFromVault() {
      swal({
        title: "Are you sure?",
        text:
          "Click 'Ok' to confirm you wish to remove this keep from this vault.",
        icon: "warning",
        buttons: true,
        dangerMode: true
      }).then(willDelete => {
        if (willDelete) {
          let data = this.$store.dispatch(
            "deleteKeepFromVault",
            this.keep.vaultKeepId
          );
          swal("Your keep has been removed!", {
            icon: "success"
          });
          this.edit = false;
        } else {
          swal("Deletion cancelled");
        }
      });
    }
  }
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