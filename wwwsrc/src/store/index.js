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
    myKeeps: [],
    activeKeep: {},
    vaults: [],
    vaultKeeps: [],
    activeVault: {}
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
    setMyKeeps(state, keeps) {
      state.myKeeps = keeps
    },
    setVaultsKeeps(state, keeps) {
      state.vaultKeeps = keeps
    },
    removeKeepFromVault(state, vaultKeepId) {
      let index = state.vaultKeeps.findIndex(vk => vk.vaultKeepId == vaultKeepId)
      state.vaultKeeps.splice(index, 1)
    },

    //#endregion
    //#region VAULTS
    setVaults(state, vaults) {
      state.vaults = vaults
    },
    addNewVault(state, newVault) {
      state.vaults.push(newVault)
    },
    removeVault(state, vaultId) {
      let index = state.vaults.findIndex(v => v.id == vaultId)
      state.vaults.splice(index, 1)
    },
    setActiveVault(state, vault) {
      state.activeVault = vault
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
    async getActiveKeep({ commit, dispatch }, keepId) {
      try {
        let res = await api.get("keeps/" + keepId)
        res.data.views++
        commit("setActiveKeep", res.data)
        setTimeout(function () { dispatch("updateKeepCount", res.data) }, 1500);
        // dispatch("editKeep", res.data)
      } catch (error) {
        console.error(error)
      }
    },
    async getMyKeeps({ commit }) {
      try {
        let res = await api.get("keeps/user")
        commit("setMyKeeps", res.data)
      } catch (error) {
        console.error(error)
      }
    },
    async deleteKeep({ }, id) {
      await api.delete("keeps/" + id)
      router.push({ name: "home" });
    },
    async editKeep({ commit }, keep) {
      let res = await api.put("keeps/" + keep.id, keep)
      commit("setActiveKeep", res.data)
    },
    async updateKeepCount({ }, keep) {
      let res = await api.put("keeps/" + keep.id + "/counts", keep)
    },
    async getKeepsByVaultId({ commit }, vaultId) {
      try {
        let res = await api.get("vaults/" + vaultId + "/keeps")
        commit("setVaultsKeeps", res.data)
      } catch (error) {
        console.error(error)
      }
    },
    async deleteKeepFromVault({ commit }, vaultKeepId) {
      try {
        let res = await api.delete("vaultKeeps/" + vaultKeepId)
        commit("removeKeepFromVault", vaultKeepId)
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
    },
    async deleteVault({ commit }, vaultId) {
      let res = await api.delete("vaults/" + vaultId)
      commit("removeVault", vaultId)
    },
    async addToVault({ commit, dispatch }, DTO) {
      try {
        await api.post("vaultKeeps", DTO)
        DTO.keep.keeps++
        setTimeout(function () { dispatch("updateKeepCount", DTO.keep) }, 1500);
      } catch (error) {
        console.error(error)
      }
    },
    async setActiveVault({ commit }, vaultId) {
      try {
        let res = await api.get("vaults/" + vaultId)
        commit("setActiveVault", res.data)
      } catch (error) {
        console.error(error)
      }
    }
    //#endregion
  }
});
