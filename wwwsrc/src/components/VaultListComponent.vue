<template>
  <div class="col-12">
    <div type="button" @click="showKeeps" class="bg-secondary my-2 vaultList">
      <h2>{{vault.name}}</h2>
      <h3>{{vault.description}}</h3>
    </div>
  </div>
</template>

<script>
export default {
  name: "VaultListComp",
  props: ["vault"],
  methods: {
    showKeeps() {
      getKeepsByVaultId("getKeepsByVaultId", this.vault.id);
    }
  }
};
</script>

<style scoped>
.vaultList {
  border-radius: 10px;
}
</style>