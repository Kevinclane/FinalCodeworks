<template>
  <div class="home container bg-secondary">
    <div class="row">
      <div class="col text-center">
        <h1>Welcome to the Vault of Keeps</h1>
      </div>
    </div>
    <div class="row" v-if="$auth.isAuthenticated">
      <div class="col-12 text-center">
        <h3>Post a keep</h3>
      </div>
      <div class="col-12 d-flex justify-content-center">
        <form @submit.prevent="createNewKeep" class="form-inline">
          <input
            class="mx-1 h-100 rounded"
            v-model="newKeep.name"
            type="text"
            id="name"
            placeholder="name"
          />
          <input
            class="mx-1 h-100 rounded"
            v-model="newKeep.description"
            type="text"
            id="description"
            placeholder="description"
          />
          <input
            class="mx-1 h-100 rounded"
            v-model="newKeep.img"
            type="imgUrl"
            id="img"
            placeholder="ImageUrl"
          />
          <button class="btn btn-primary" type="submit">submit</button>
        </form>
      </div>
    </div>

    <div class="card-columns mt-3">
      <keep v-for="keep in keeps" :key="keep.id" :keep="keep" />
    </div>

    <!-- d-flex justify-content-around -->
  </div>
</template>

<script>
import Keep from "@/components/KeepComponent.vue";
export default {
  name: "home",
  data() {
    return {
      newKeep: {},
    };
  },
  mounted() {
    this.$store.dispatch("getPublicKeeps");
  },
  computed: {
    user() {
      return this.$store.state.user;
    },
    keeps() {
      return this.$store.state.publicKeeps;
    },
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
    createNewKeep() {
      this.$store.dispatch("createNewKeep", this.newKeep);
      this.newKeep = {};
    },
  },
  components: {
    Keep,
  },
};
</script>

<style scoped>
/* .card-columns {
  @include media-breakpoint-only(lg) {
    column-count: 4;
  }
  @include media-breakpoint-only(xl) {
    column-count: 5;
  }
} */
</style>
