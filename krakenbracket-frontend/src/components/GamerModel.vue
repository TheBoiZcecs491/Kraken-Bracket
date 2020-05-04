<template>
  <div class="gamer-card">
    <h2 class="gamer-name">{{ gamer.gamerTag }}#{{ gamer.gamerTagID }}</h2>
    <br />
    <!-- <router-link
      class="gamer-link"
      :to="{ name: 'bracket-view', params: { id: bracket.bracketID } }"
    >
      <v-btn class="primary" color="#2196F3">View Gamer</v-btn>
    </router-link> -->
  </div>
</template>

<script>
export default {
  props: {
    gamer: Object
  }
};
</script>

<style>
.gamer-card {
  padding: 20px;
  padding-top: 30px;
  margin-bottom: 24px;
  transition: all 0.2s linear;
}
.gamer-link {
  color: black;
  text-decoration: none;
  font-weight: 100;
}
.gamer-name {
  padding-bottom: 10px;
  font-style: bold;
  font-family: Verdana, Geneva, Tahoma, sans-serif;
}
</style>
