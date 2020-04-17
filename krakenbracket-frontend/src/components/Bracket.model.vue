<template>
    <router-link :to="{name : 'bracket-view', params: {id: bracket.id}}">
    <div>
        <h2>Bracket Name: {{bracket.bracket_name}}</h2>
        <h4>Bracket ID: {{bracket.id}}</h4>
        <h4>Game Played: {{bracket.game_played}}</h4>
        <h3>Number of competitors: {{bracket.number_player}}</h3>
        <p>------------------------</p>
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