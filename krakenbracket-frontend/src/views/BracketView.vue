<template>
    <div>
        <h1>Showing bracket ID #{{id}}</h1>
        <h2>Bracket Name: {{bracket.bracket_name}}</h2>
        <h4>Bracket ID: {{bracket.id}}</h4>
        <h4>Game Played: {{bracket.game_played}}</h4>
        <h3>Number of competitors: {{bracket.number_player}}</h3>
        <p>------------------------</p>
    </div>
</template>
<script>
import BracketService from '@/services/BracketService.js'
export default {
    props: ['id'],
    data() {
        return{
            bracket: {}
        }
    },
    created(){
        BracketService.getBracketByID(this.id)
        .then(response => {
            this.bracket = response.data
        })
        .catch(error => {
            console.log("Error: " + error.response);
        })
    }
}
</script>