<template>
  <div class="col-12">
    <div class="bg-secondary my-2 vaultList">
      <h2 class="bg-light rounded" type="button" v-on:click="addToVault">{{vault.name}}</h2>
    </div>
  </div>
</template>

<script>
export default {
  name: "VaultListComp",
  props: ["vault", "closeModal"],
  mounted() {
    this.getKeepsByVaultId;
  },
  methods: {
    async addToVault() {
      await this.$store.dispatch("getKeepsByVaultId", this.vault.id);
      let vaultKeeps = this.$store.state.vaultKeeps;
      let activeKeep = this.$store.state.activeKeep;
      let hasKeep;
      let found = vaultKeeps.find(k => k.id == activeKeep.id);
      if (found) {
        hasKeep = true;
        this.closeModal(
          "This keep is already in vault:" + " " + this.vault.name,
          hasKeep
        );
      } else {
        hasKeep = false;
        let DTO = {
          keepId: this.$store.state.activeKeep.id,
          vaultId: this.vault.id,
          keep: this.$store.state.activeKeep
        };
        this.$store.dispatch("addToVault", DTO);
        this.closeModal(
          "This keep has been succussfully add to the vault: " +
            " " +
            this.vault.name,
          hasKeep
        );
      }
    },
    getKeepsByVaultId() {
      this.$store.dispatch("getKeepsByVaultId", this.vaultId, true);
    }
  },
  computed: {
    keepsInVault() {
      return this.$store.state.vaultKeeps;
    }
  }
};
</script>

<style scoped>
.vaultList {
  border-radius: 10px;
}
</style>