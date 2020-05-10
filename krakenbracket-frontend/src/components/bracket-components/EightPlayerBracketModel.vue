<template>
    <div>
        <v-btn @click="updatePlayerBracketPlacements">Update players</v-btn>
        <bracket :rounds="rounds">
            <template #player="{ player }">
                {{ player.name }}
            </template>
        </bracket>
        <!-- <p>{{competitors}}</p> -->
        <h3>Placements:</h3>
        <ul v-for="competitor in competitors" :key="competitor.score">
            <li>{{competitor.gamerTag}}  {{competitor.score}}</li>
        </ul>
    </div>
  
</template>

<script>
import Bracket from "@/components/bracket-components/Bracket.vue";
import BracketService from "@/services/BracketService.js";
// import axios from "axios";

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
                    player1: { id: "1", name: "?", winner: false },
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
                    player1: { id: "1", name: "?", winner: false },
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
      console.log(this.competitors);
      setTimeout(() => {
           var competitorList = new Array(8);
    //   console.log(competitorList);
    for (let index = 0; index < this.competitors.length; index++) {
        competitorList[index] = this.competitors[index].gamerTag;
    }
     for (let index = this.competitors.length; 
            index < competitorList.length; index++) {
        competitorList[index] = "?";
    }
    // Algorithms used to populate the bracket display
    
    
    // Quarter finals
    var j = 0
    for (let i = 0; i < 6; i++) {
        if(i !== 0) i++;
        this.rounds[0].games[j].player1.name = competitorList[i];
        this.rounds[0].games[j].player2.name = competitorList[i + 1];
        j++;
    }

    // Semi finals
    for (let i = 0; i < 4; i++) {
        if(this.competitors[i].score == undefined) continue;
        else{
            if(this.competitors[i].score == 1){
            if(i % 2 == 0){
                this.rounds[1].games[0].player1.name = this.competitors[i].gamerTag;
            }
            else {
                this.rounds[1].games[0].player2.name = this.competitors[i].gamerTag;
            }
        }
    }
        
    }
    for (let i = 4; i < 8; i++) {
        console.log(this.competitors[i].score)
        if(this.competitors[i].score == undefined) continue;
        else{
            if(this.competitors[i].score == 1){
            if(i % 2 == 0){
                this.rounds[1].games[1].player1.name = this.competitors[i].gamerTag;
            }
            else {
                this.rounds[1].games[1].player2.name = this.competitors[i].gamerTag;
            }
        }
        }
    }

    
    // Finals
    var player1;
    var player2;
    for (let i = 0; i < 8; i++) {
        if(this.competitors[i].score == 2){
            player1 =this.competitors[i];
            break;
        }
    }
    for (let i = 0; i < 8; i++) {
        if(this.competitors[i].score == 2 && (this.competitors[i] !== player1)){
            player2 =this.competitors[i];
            break;
        }
    }
    this.rounds[2].games[0].player1.name = player1.gamerTag;
    this.rounds[2].games[0].player2.name = player2.gamerTag;
      }, 100);
  },
  methods:{
      updatePlayerBracketPlacements(){
          var bracketLayer = prompt("Enter the bracket layer number");
          var matchNumber = prompt("Enter the match number");
          var gamerTag = prompt("Enter the gamerTag");
          var playerPlacement = prompt("Player 1 or 2?");
          if(playerPlacement == 1){
              this.rounds[bracketLayer].games[matchNumber].player1.name = gamerTag;
              for (let i = 0; i < this.competitors.length; i++) {
                  console.log("TEST1")
                  if(this.competitors[i].gamerTag == gamerTag){
                  BracketService.updateBracketStandings(this.competitors[0].bracketID, this.competitors[i])
              }
                  
              }
          }
          else if (playerPlacement == 2){
              this.rounds[bracketLayer].games[matchNumber].player2.name = gamerTag;
              for (let i = 0; i < this.competitors.length; i++) {
                  if (this.competitors[i].gamerTag == gamerTag){
                      console.log("TEST2" + this.competitors[i] == gamerTag)
                      BracketService.updateBracketStandings(this.competitors[0].bracketID, this.competitors[i])
                  }
              }
          }
          else {
              console.log("ERROR");
          }
      }
  },
  computed:{
      competitorStandings(){
          return this.competitors.orderBy(this.competitors, "value", "desc");
      }
  }
}
</script>
