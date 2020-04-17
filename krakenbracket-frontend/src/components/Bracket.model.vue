<template>
    <router-link class="bracket-link" :to="{name : 'bracket-view', params: {id: bracket.id}}">
    <div class="bracket-card">
        <h2>{{bracket.bracket_name}}</h2>
        <h3>Start Date: {{bracket.start_date}}</h3>
        <h3>End Date: {{bracket.end_date}}</h3>
        <h4>Number of players: {{bracket.number_player}}</h4>
    </div>
    </router-link>
</template>

<script>

export default {
    props: {
        bracket: Object
    }
}
</script>

<style>
.bracket-card {
  padding: 20px;
  margin-bottom: 24px;
  transition: all 0.2s linear;
  cursor: pointer;
}
.bracket-card:hover {
  transform: scale(1.01);
  box-shadow: 0 3px 12px 0 rgba(0, 0, 0, 0.2), 0 1px 15px 0 rgba(0, 0, 0, 0.19);
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