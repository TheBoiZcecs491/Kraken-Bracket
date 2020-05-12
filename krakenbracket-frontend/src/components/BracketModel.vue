<template>
  <div class="bracket-card">
    <h2 class="bracket-name">{{ bracket.bracketName }}</h2>
    <h3>Start Date: {{ bracket.startDate }}</h3>
    <h3>End Date: {{ bracket.endDate }}</h3>
    <h3>Host: {{ bracket.host }}</h3>
    <h4>
      Player Capacity: {{ bracket.maxCapacity }}
      <span v-show="bracket.maxCapacity === 128">(MAX)</span>
    </h4>
    <h4>Video Game: {{ bracket.gamePlayed }}</h4>
    <h5>Rules: {{ bracket.rules }}</h5>
    <br />
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
  padding-top: 30px;
  margin-bottom: 24px;
  transition: all 0.2s linear;
  padding-left: 150px;
  padding-right: 150px;
}
.bracket-link {
  color: black;
  text-decoration: none;
  font-weight: 100;
}
.bracket-name {
  padding-bottom: 10px;
  font-style: italic;
  font-family: Verdana, Geneva, Tahoma, sans-serif;
}
</style>
