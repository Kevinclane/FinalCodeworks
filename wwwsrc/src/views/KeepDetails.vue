<template>
  <div class="container-fluid text-black my-3">
    <div class="row">
      <div class="col text-center mt-3 cardDesign">
        <img :src="keep.img" alt="error loading image" class="w-100 imgBorder" />
      </div>
    </div>
    <div class="row bg-secondary w-100 m-auto roundBottom py-2">
      <div class="col-6">
        <div class="pb-2">
          <h3 class="ml-2">{{keep.name}}</h3>
          <h3 class="ml-2">{{keep.description}}</h3>
          <h5 class="ml-2">Views: {{keep.views}}</h5>
          <h5 class="ml-2">Keeps: {{keep.keeps}}</h5>
        </div>
      </div>
      <div class="col-3 offset-3 d-flex flex-flow-column-reverse align-items-end">
        <button v-if="isCreator" class="btn btn-danger my-2 w-50" @click="deleteKeep">Delete Keep</button>
        <div v-if="keep.isPrivate == true">
          <p>
            Current privacy:
            <span class="color-red">Private</span>
          </p>
          <button @click="makePublic" class="btn btn-info my-2 w-100">Make public</button>
        </div>
        <div v-if="keep.isPrivate == false">
          <p>
            Current privacy:
            <span class="color-green">Public</span>
          </p>
          <button @click="makePrivate" class="btn btn-info my-2 w-100">Make private</button>
        </div>
      </div>
      <div class="col-12 text-center">
        <i
          class="fa fa-heart fa-2x color-red"
          aria-hidden="true"
          v-if="$auth.isAuthenticated"
          type="button"
          @click="showModal=true"
        ></i>
      </div>
    </div>
    <transition name="fade" appear>
      <div v-if="showModal" class="modal-overlay" id="vaultListModal">
        <div class="container-fluid text-center">
          <div class="row">
            <i
              class="fa fa-times fa-2x x-close color-red d-flex align-items-center"
              type="button"
              @click="showModal=false"
              aria-hidden="true"
            ></i>
            <div class="col">
              <div>
                <h3 class="text-black">Create a new vault</h3>
              </div>
              <div>
                <form
                  @submit.prevent="createNewVault"
                  class="form-inline d-flex justify-content-center"
                >
                  <input
                    v-model="newVault.name"
                    type="text"
                    id="name"
                    placeholder="name"
                    class="h-100 rounded"
                  />
                  <input
                    v-model="newVault.description"
                    type="text"
                    id="description"
                    placeholder="description"
                    class="h-100 rounded"
                  />
                  <button class="btn btn-success">Submit</button>
                </form>
              </div>
            </div>
          </div>
          <div class="row">
            <vault v-for="vault in vaults" :key="vault.id" :vault="vault" :closeModal="addToVault" />
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
      newVault: {},
    };
  },
  mounted() {
    this.$store.dispatch("getActiveKeep", this.$route.params.keepId);
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
    },
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
        dangerMode: true,
      }).then((willDelete) => {
        if (willDelete) {
          let data = this.$store.dispatch("deleteKeep", this.keep.id);
          swal("Your keep has been deleted!", {
            icon: "success",
          });
          this.edit = false;
        } else {
          swal("Deletion cancelled");
        }
      });
    },
    addToVault: function (response, bool) {
      if (bool == true) {
        swal({
          title: response,
          icon: "warning",
          button: "close",
          dangerMode: true,
        });
      } else if (bool == false) {
        swal({
          title: response,
          icon: "success",
          button: "close",
        });
        this.showModal = false;
      }
    },
    makePublic() {
      this.keep.isPrivate = false;
      this.$store.dispatch("editKeep", this.keep);
    },
    makePrivate() {
      this.keep.isPrivate = true;
      this.$store.dispatch("editKeep", this.keep);
    },
  },
  components: {
    Vault,
  },
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
  left: 30vw;
  bottom: 10vh;
  right: 30vw;
  z-index: 100;
  border-radius: 20px;
  max-height: 80vh;
  background-color: #444;
  overflow-y: auto;
  overflow-x: hidden;
  border: 4px solid black;
}
.color-red {
  color: red;
}
.color-green {
  color: green;
}
.text-black {
  color: black;
}
.x-close {
  position: absolute;
  top: 4px;
  right: 8px;
  z-index: 100;
}
.flex-flow-column-reverse {
  flex-flow: column-reverse;
}
</style>