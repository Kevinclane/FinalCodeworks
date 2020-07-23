<template>
  <div class="dashboard container">
    <div class="row my-3">
      <div class="col-12">
        <h1 class="text-center">WELCOME TO YOUR DASHBOARD</h1>
      </div>
    </div>
    <div class="row my-3">
      <div class="col-3">
        <div class="row bg-secondary p-3 roundedCstm">
          <h3 class="col-12 text-center">Your Vaults</h3>
          <vault v-for="vault in vaults" :key="vault.id" :vault="vault" />
        </div>
      </div>
      <div class="col-8 offset-1 bg-secondary p-3 roundedCstm">
        <div class="row">
          <div class="col-12">
            <h3 class="text-center">Your Keeps</h3>
            <div class="card-columns">
              <keep v-for="keep in myKeeps" :key="keep.id" :keep="keep" />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Keep from "@/components/KeepComponent.vue";
import Vault from "@/components/DashVaultComponent.vue";
export default {
  name: "Dashboard",
  data() {
    return {
      newVault: {}
    };
  },
  mounted() {},
  computed: {
    myKeeps() {
      return this.$store.state.myKeeps;
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
    Vault,
    Keep
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
.roundedCstm {
  border-radius: 20px;
  border: 2px solid black;
}
</style>
