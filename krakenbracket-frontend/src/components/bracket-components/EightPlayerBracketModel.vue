<template>
    <div>
        <bracket :rounds="rounds">
            <template #player="{ player }">
                {{ player.name }}
            </template>
        </bracket>
        <!-- <p v-for="competitor in competitors" 
        :key="competitor.bracketID" 
        :competitor="competitor">{{competitor.gamerTag}}</p> -->
        <p>{{competitors}}</p>
        <!-- <button @click="test">Test</button> -->
        <!-- <p>{{bracket}}</p> -->
    </div>
  
</template>

<script>
import Bracket from "@/components/bracket-components/Bracket.vue";
export default {
  props:{
    competitors: Array
  },
  components: {
    Bracket
  },
  data() {
    return {
      rounds: [
          {
            games: [
                {
                    player1: { name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                }
            ]
        },
        //Semi
        {
            games: [
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                }
            ]
        },
        //Final
        {
            games: [
                {
                    player1: { name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                }
            ]
        }]
    }
  },
  created(){
      var competitorList = new Array(8);
    //   console.log(competitorList);
    for (let index = 0; index < this.competitors.length; index++) {
        competitorList[index] = this.competitors[index].gamerTag;
    }
     for (let index = this.competitors.length; 
            index < competitorList.length; index++) {
        competitorList[index] = "?";
    }
    console.log(competitorList)
  }
}
</script>
