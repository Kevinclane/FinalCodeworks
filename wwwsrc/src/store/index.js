import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";
import router from "../router";

Vue.use(Vuex);

let baseUrl = location.host.includes("localhost")
  ? "https://localhost:5001/"
  : "/";

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
});

export default new Vuex.Store({
  state: {
    user: {},
    publicKeeps: [],
    activeKeep: {},
    vaults: []
  },
  mutations: {
    setActiveUser(state, user) {
      state.user = user
    },
    //#region KEEPS
    addPublicKeeps(state, keeps) {
      state.publicKeeps = keeps
    },
    addNewKeep(state, newKeep) {
      state.publicKeeps.push(newKeep)
    },
    setActiveKeep(state, keep) {
      state.activeKeep = keep
    },

    //#endregion
    //#region VAULTS
    setVaults(state, vaults) {
      state.vaults = vaults
    },
    addNewVault(state, newVault) {
      state.vaults.push(newVault)
    }
    //#endregion
  },
  actions: {
    setBearer({ }, bearer) {
      api.defaults.headers.authorization = bearer;
    },
    resetBearer() {
      api.defaults.headers.authorization = "";
    },
    setUser({ commit }, user) {
      commit("setActiveUser", user)
    },

    //#region KEEPS
    async getPublicKeeps({ commit }) {
      try {
        let res = await api.get("keeps")
        commit("addPublicKeeps", res.data)
      } catch (error) {
        console.error(error)
      }
    },
    async createNewKeep({ commit }, newKeep) {
      try {
        let res = await api.post("keeps", newKeep)
        commit("addNewKeep", res.data)
      } catch (error) {
        console.error(error)
      }
    },
    async getActiveKeep({ commit }, keepId) {
      try {
        let res = await api.get("keeps/" + keepId, keepId)
        commit("setActiveKeep", res.data)
      } catch (error) {
        console.error(error)
      }
    },

    //#endregion

    //#region VAULTS
    async getMyVaults({ commit }) {
      let res = await api.get("vaults")
      commit("setVaults", res.data)
    },
    async createNewVault({ commit }, newVault) {
      let res = await api.post("vaults", newVault)
      commit("addNewVault", res.data)
    }
    //#endregion
  }
});
