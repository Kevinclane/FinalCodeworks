<template>
  <div class="col">
    <div class="bg-primary my-2">
      <h2>{{vault.name}}</h2>
      <h3>{{vault.description}}</h3>
    </div>
  </div>
</template>

<script>
export default {
  name: "VaultListComp",
  props: ["vault"]
};
</script>