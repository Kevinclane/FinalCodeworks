<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col text-center mt-3 cardDesign">
        <img :src="keep.img" alt="error loading image" class="w-100 imgBorder" />
      </div>
    </div>
    <div class="row bg-secondary roundBottom py-2">
      <div class="col-6">
        <div class="pb-2">
          <h3 class="ml-2">{{keep.name}}</h3>
          <h3 class="ml-2">{{keep.description}}</h3>
          <h5 class="ml-2">Views: {{keep.views}}</h5>
          <h5 class="ml-2">Shares: {{keep.shares}}</h5>
        </div>
      </div>
      <div class="col-3 offset-3 d-flex flex-column-reverse justify-content-end">
        <button v-if="isCreator" class="btn btn-danger" @click="deleteKeep">Delete Keep</button>
      </div>
      <div class="col-12 text-center">
        <button
          v-if="$auth.isAuthenticated"
          class="btn btn-primary"
          @click="showModal = true"
        >Add to favorites</button>
      </div>
    </div>
    <transition name="fade" appear>
      <div v-if="showModal" class="modal-overlay">
        <div class="container-fluid text-center">
          <div class="row">
            <div class="col">
              <div>
                <h3>Create a new vault</h3>
              </div>
              <div>
                <form @submit.prevent="createNewVault" class="form-inline">
                  <input v-model="newVault.name" type="text" id="name" placeholder="name" />
                  <input
                    v-model="newVault.description"
                    type="text"
                    id="description"
                    placeholder="description"
                  />
                  <button>Submit</button>
                </form>
              </div>
            </div>
          </div>
          <div class="row">
            <vault v-for="vault in vaults" :key="vault.id" :vault="vault" />
          </div>
        </div>
      </div>
    </transition>
  </div>
</template>
<script>
import Vault from "@/components/VaultListComponent.vue";
import swal from "sweetalert";
export default {
  name: "KeepDetails",
  data() {
    return {
      showModal: false,
      newVault: {}
    };
  },
  mounted() {
    // debugger;
    this.$store.dispatch("getActiveKeep", this.$route.params.keepId);
    // this.$store.dispatch("setUser", this.$auth.user);
    // if (this.user) {
    //   this.$store.dispatch("getMyVaults");
    // }
    console.log(this.$auth.user);
  },
  computed: {
    keep() {
      return this.$store.state.activeKeep;
    },
    vaults() {
      return this.$store.state.vaults;
    },
    user() {
      return this.$store.state.user;
    },
    isCreator() {
      return this.user.sub == this.keep.userId;
    },
    auth() {
      return this.$auth.user;
    }
  },
  methods: {
    favorite() {
      this.$store.dispatch("addToFavorites", this.keep);
    },
    createNewVault() {
      this.$store.dispatch("createNewVault", this.newVault);
      this.newVault = {};
    },
    deleteKeep() {
      swal({
        title: "Are you sure?",
        text: "Click 'Ok' to confirm you wish to delete this keep.",
        icon: "warning",
        buttons: true,
        dangerMode: true
      }).then(willDelete => {
        if (willDelete) {
          let data = this.$store.dispatch("deleteKeep", this.keep.id);
          swal("Your keep has been deleted!", {
            icon: "success"
          });
          this.edit = false;
        } else {
          swal("Deletion cancelled");
        }
      });
    }
  },
  components: {
    Vault
  }
};
</script>
<style scoped>
.cardDesign {
  /* border-radius: 30px; */
  border-top-left-radius: 30px;
  border-top-right-radius: 30px;
}
.imgBorder {
  border-radius: inherit;
  size: fill;
}
.roundBottom {
  border-bottom-left-radius: 30px;
  border-bottom-right-radius: 30px;
}
.modal-overlay {
  position: fixed;
  top: 10vh;
  left: 10vw;
  bottom: 10vh;
  right: 10vw;
  z-index: 100;
  border-radius: 20px;
  max-height: 80vh;
  background-color: lightgoldenrodyellow;
  overflow-y: auto;
  overflow-x: hidden;
}
</style>