<template>
  <div class="container">
    <div v-if="isCreator" class="row">
      <div class="col-12">
        <div class="card-columns mt-3">
          <keep v-for="keep in keeps" :key="keep.id" :keep="keep" />
        </div>
      </div>
    </div>
    <div v-else class="row">
      <div class="col-12 text-center">
        <h1>Sorry, but this is not your vault</h1>
      </div>
    </div>
  </div>
</template>
<script>
import Keep from "@/components/VaultKeepComponent.vue";
export default {
  name: "VaultDetails",
  data() {
    return {};
  },
  mounted() {
    this.$store.dispatch("setActiveVault", this.$route.params.vaultId);
    this.$store.dispatch("getKeepsByVaultId", this.$route.params.vaultId);
  },
  computed: {
    keeps() {
      return this.$store.state.vaultKeeps;
    },
    vault() {
      return this.$store.state.activeVault;
    },
    isCreator() {
      return this.$store.state.activeVault.userId == this.$store.state.user.sub;
    },
  },
  methods: {},
  components: {
    Keep,
  },
};
</script>

<style scoped>
</style>