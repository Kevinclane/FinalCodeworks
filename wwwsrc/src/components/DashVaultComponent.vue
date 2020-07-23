<template>
  <div class="col-12 my-3 bg-primary text-black d-flex align-items-center text-center roundedCstm">
    <i
      class="fa fa-minus minus-close color-red d-flex align-items-center"
      type="button"
      @click="deleteVault"
      aria-hidden="true"
    ></i>
    <router-link :to="{ name: 'vaultDetails', params: { vaultId: vault.id } }">
      <h4 class="text-black">{{vault.name}}</h4>
      <h5 class="text-black">{{vault.description}}</h5>
    </router-link>
  </div>
</template>
<script>
import swal from "sweetalert";
export default {
  name: "DashVault",
  props: ["vault"],
  methods: {
    deleteVault() {
      swal({
        title: "Are you sure?",
        text: "Click 'Ok' to confirm you wish to delete this vault.",
        icon: "warning",
        buttons: true,
        dangerMode: true
      }).then(willDelete => {
        if (willDelete) {
          let data = this.$store.dispatch("deleteVault", this.vault.id);
          swal("Your vault has been deleted!", {
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
.text-black {
  color: black;
}
.roundedCstm {
  border-radius: 20px;
  flex-direction: column;
}
a {
  text-decoration: none;
}
.minus-close {
  position: absolute;
  top: 4px;
  right: 8px;
  z-index: 100;
}
</style>