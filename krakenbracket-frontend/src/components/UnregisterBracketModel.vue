<template>
  <div class="text-xs-center">
    <v-dialog v-model="dialog" width="600">
      <template v-slot:activator="{ on }">
        <v-btn color="red lighten-2" dark v-on="on">
          unregister
        </v-btn>
      </template>

      <v-card>
        <v-card-title class="headline grey lighten-2" primary-title>
          Confirm Unregistration
        </v-card-title>
        <!-- 
        Status codes:
        2 - bracket is in progress
        1 - bracket not in progress and has already completed
        0 - bracket not in progress and has not begun
                 -->
        <div style="text-align: center;">
        <v-card-text v-if="bracket.statusCode === 0">
          <p>
            The bracket has not begun yet. Are you sure you want to unregister?
          </p>
        </v-card-text>
        <v-card-text v-else-if="bracket.statusCode === 1">
          <p>Unregistration disabled. The bracket has already been completed</p>
        </v-card-text>
        <v-card-text v-else-if="bracket.statusCode === 2">
          <p>Bracket is in progress. Are you sure you want to unregister?</p>
        </v-card-text>
        </div>
        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" flat @click="dialog = false">
            I accept
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import BracketService from "@/services/BracketService.js";
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
};
</script>
