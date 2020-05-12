<template>
  <div class="text-xs-center">
    <v-dialog v-model="dialog" width="600">
      <template v-slot:activator="{ on }">
        <v-btn color="red lighten-2" dark v-on="on">
          Delete Bracket
        </v-btn>
      </template>

      <v-card>
        <v-card-title class="headline grey lighten-2" primary-title>
          Confirm Bracket Deletion
        </v-card-title>
        <!-- 
        Status codes:
        2 - bracket is in progress
        1 - bracket not in progress and has already completed
        0 - bracket not in progress and has not begun
                 -->
        <div style="text-align: center;">
          
          <v-card-text v-if="bracket.statusCode === 1">
            <p>
              Bracket has completed, further changes are not permitted.
            </p>
          </v-card-text>
          <v-card-text v-else-if="bracket.statusCode === 2">
            <p>Bracket is in progress. Are you sure you want to delete?</p>
          </v-card-text>
        </div>
        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" flat @click="deleteBracket">
            I Accept
          </v-btn>
          <v-btn color="error" flat @click="dialog = false">Cancel</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import BracketService from "@/services/BracketService.js";
import axios from "axios";
export default {
  props: {
    bracket: Object
  },
  data() {
    return {
      dialog: false
    };
  },
  created() {
    BracketService.getBracketByID(this.id)
      .then(response => {
        this.bracket = response.data;
      })
      .catch(error => {
        console.log("Error: " + error.response);
      });
  },
  methods: {
    deleteBracket() {
      if (this.bracket.statusCode == 2) {
        // If bracket is in progress
        var reason = prompt(
          "Please enter reason for deleting in-progress bracket"
        );
        this.bracket.reason = reason;
        var cancelledTitle = "[Cancelled] " + this.bracket.bracketName;
      } 
      else if (this.bracket.statusCode == 1) {
        // If bracket has already ended
        alert(
          "This bracket has already ended, further changes are not permitted."
        );
      }
        // If bracket has not started yet
        axios.put(`https://localhost:44352/api/brackets/deleteBracket/`, {
          BracketID: this.bracket.bracketID,
          BracketName: cancelledTitle,
          Host: this.bracket.host,
          BracketTypeID: this.bracket.bracketTypeID,
          PlayerCount: this.bracket.playerCount,
          GamePlayed: this.bracket.gamePlayed,
          GamingPlatform: this.bracket.gamingPlatform,
          Rules: this.bracket.rules,
          StartDate: this.bracket.startDate,
          EndDate: this.bracket.endDate,
          StatusCode: this.bracket.statusCode,
          MaxCapacity: this.bracket.maxCapacity,
          Reason: this.bracket.reason
        });
        this.dialog=false;
    }
  }
};
</script>
