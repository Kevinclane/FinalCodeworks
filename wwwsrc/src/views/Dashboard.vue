<template>
  <div class="dashboard">
    <h1>WELCOME TO YOUR DASHBOARD</h1>
    <button @click="showVaultModal = true" class="btn btn-primary">Vaults</button>
    <transition name="fade" appear>
      <div v-if="showVaultModal" class="modal-overlay">
        <div class="container-fluid text-center">
          <div class="row">
            <i
              class="fa fa-times x-close d-flex align-items-center mt-1"
              type="button"
              @click="showVaultModal = false"
            ></i>
            <div class="col">
              <div>
                <h3>Create a new vault</h3>
              </div>
              <div>
                <form @submit.prevent="createNewVault" class="form-inline">
                  <input
                    class="rounded"
                    v-model="newVault.name"
                    type="text"
                    id="name"
                    placeholder="name"
                  />
                  <input
                    class="rounded"
                    v-model="newVault.description"
                    type="text"
                    id="description"
                    placeholder="description"
                  />
                  <button class="btn btn-success">Submit</button>
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
export default {
  name: "Dashboard",
  data() {
    return {
      showVaultModal: false,
      newVault: {}
    };
  },
  mounted() {
    this.$store.dispatch("getPublicKeeps");
  },
  computed: {
    userKeeps() {
      return this.$store.state.myKeeps;
    },
    publicKeeps() {
      return this.$store.state.publicKeeps;
    },
    user() {
      return this.$store.state.user;
    },
    vaults() {
      return this.$store.state.vaults;
    }
  },
  methods: {
    createNewVault() {
      this.$store.dispatch("createNewVault", this.newVault);
      this.newVault = {};
    }
  },
  components: {
    Vault
  }
};
</script>

<style scoped>
.modal-overlay {
  position: fixed;
  top: 10vh;
  left: 10vw;
  bottom: 10vh;
  right: 10vw;
  z-index: 100;
  border-radius: 20px;
  max-height: 80vh;
  background-color: rgb(187, 187, 186);
  overflow-y: auto;
  overflow-x: hidden;
}
.x-close {
  position: absolute;
  top: 2px;
  right: 8px;
  color: red;
  z-index: 101;
}
</style>
