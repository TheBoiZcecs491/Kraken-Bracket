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
              // Quarter finals
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
        //Semi finals
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
        //Finals
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
    console.log(this.rounds[0].games[0].player1.name)

    for (let i = 0; i < this.competitors.length; i++) {
        for (let j = 0; j < 2; j++) {
            if(j % 2 === 0){
                this.rounds[0].games[i].player1.name = competitorList[i + j];
            }
            else{
                this.rounds[0].games[i].player2.name = competitorList[i + j];
            }
        }
        
        // else {
        //     // this.rounds[0].games[index].player1.name = competitorList[index];
        //     this.rounds[0].games[index].player2.name = competitorList[index];
        // }
        
    }
  }
}
</script>
