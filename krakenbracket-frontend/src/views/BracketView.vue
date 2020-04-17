<template>
    <div>
        <h1 id="title">{{bracket.bracket_name}}</h1>
        <h3>Start Date: {{bracket.start_date}}</h3>
        <h3>End Date: {{bracket.end_date}}</h3>
        <h4>Number of players: {{bracket.number_player}}</h4>
        <h4>Game: {{bracket.game_played}}</h4>
        <h4>Gaming platform: {{bracket.gaming_platform}}</h4>
        <div>
            <h4>Rules:</h4>
            <p>{{bracket.rules}}</p>
        </div>
        <button>Register!</button>
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

<style scoped>
#title{
    font-size: 100px;
    font-weight: 800;
}
</style>