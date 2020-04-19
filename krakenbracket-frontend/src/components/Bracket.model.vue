<template>
  <div class="bracket-card">
    <h2>{{ bracket.bracketName }}</h2>
    <h3>Start Date: {{ bracket.startDate }}</h3>
    <h3>End Date: {{ bracket.startDate }}</h3>
    <h4>Number of players: {{ bracket.playerCount }}</h4>
    <router-link
      class="bracket-link"
      :to="{ name: 'bracket-view', params: { id: bracket.bracketID } }"
    >
      <v-btn class="primary" color="#2196F3">View Bracket</v-btn>
    </router-link>
  </div>
</template>

<script>
export default {
  props: {
    bracket: Object
  }
};
</script>

<style>
.bracket-card {
  padding: 20px;
  margin-bottom: 24px;
  transition: all 0.2s linear;
}
.bracket-card > .title {
  margin: 0;
}
.bracket-link {
  color: black;
  text-decoration: none;
  font-weight: 100;
}
</style>
