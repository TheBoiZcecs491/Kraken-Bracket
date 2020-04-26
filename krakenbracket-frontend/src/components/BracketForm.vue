<template>
  <v-app id="inspire">
    <div v-if="!loggedIn">
      <NotLoggedIn />
    </div>
    <div v-else>
        <h1>Create a new bracket</h1>
        <v-row justify="space-around">
                <v-col class="px-4" cols="12" sm="3">
                <v-text-field
                    v-model="BracketName"
                    label="Bracket Name"
                    placeholder="EVO 2020 SFVAE Pools - 1"
                ></v-text-field>

                <v-slider
                    v-model="CompetitorCount"
                    min="2"
                    max="128"
                    label="Number of Competitors"
                ><template v-slot:append>
                        <v-text-field
                            v-model="CompetitorCount"
                            class="mt-0 pt-0"
                            hide-details
                            single-line
                            type="number"
                            style="width: 60px"
                        ></v-text-field>
                    </template>
                </v-slider>

                <v-container id="GamePlayed">
                    <v-overflow-btn
                        class="my-2"
                        :items="dropdown_gamePlayed"
                        :menu-props="topMenu ? 'top' : ''"
                        hint="Fill out if game not found"
                        label="Game played"
                        target="#dropdown_gamePlayed"
                    ></v-overflow-btn>
                </v-container>
                <v-container id="GamePlatform">
                    <v-overflow-btn
                        class="my-2"
                        :items="dropdown_gamePlatform"
                        :editable= true
                        :persistent-hint="persistentHint"
                        :menu-props="topMenu ? 'top' : ''"
                        hint="Fill out if system not found"
                        label="Platform"
                        target="#dropdown_gamePlatform"
                    ></v-overflow-btn>
                </v-container>

                <v-row>
                    <v-col cols="12" md="15">
                        <v-textarea
                        name="Rules"
                        label="Rule set"
                        hint="700 char max"
                        ></v-textarea>
                    </v-col>
                </v-row>
                
                <v-container>
                    <v-row>
                        <v-col cols="12" lg="6">
                            <v-menu
                            ref="menu1"
                            v-model="StartDate"
                            :close-on-content-click="false"
                            transition="scale-transition"
                            offset-y
                            max-width="290px"
                            min-width="290px"
                            >
                            <template v-slot:activator="{ on }">
                                <v-text-field
                                v-model="dateFormatted"
                                label="Start Date"
                                hint="MM/DD/YYYY format"
                                persistent-hint
                                @blur="date = parseDate(dateFormatted)"
                                v-on="on"
                                ></v-text-field>
                            </template>
                            <v-date-picker v-model="date" no-title @input="menu1 = false"></v-date-picker>
                            </v-menu>
                        </v-col>
                        <v-col cols="12" lg="6">
                            <v-menu
                            ref="menu1"
                            v-model="EndDate"
                            :close-on-content-click="false"
                            transition="scale-transition"
                            offset-y
                            max-width="290px"
                            min-width="290px"
                            >
                            <template v-slot:activator="{ on }">
                                <v-text-field
                                v-model="dateFormatted"
                                label="End Date"
                                hint="MM/DD/YYYY format"
                                persistent-hint
                                @blur="date = parseDate(dateFormatted)"
                                v-on="on"
                                ></v-text-field>
                            </template>
                            <v-date-picker v-model="date" no-title @input="menu1 = false"></v-date-picker>
                            </v-menu>
                        </v-col>
                    </v-row>
                </v-container>

                <v-btn x-large>confirm</v-btn>
                
                </v-col>
        </v-row>
    </div>
  </v-app>
</template>

<script>
import BracketService from "@/services/BracketService.js";
import axios from "axios";
import { authComputed } from "../store/helpers.js";
import NotLoggedIn from "../components/NotLoggedIn.vue";
export default {
  props: ["id"],
  components: {
    NotLoggedIn
  },
  data() {
    return {
      bracket: {
        BracketName: this.BracketName,
        CompetitorCount: this.CompetitorCount,
        GamePlayed: this.GamePlayed,
        GamePlatform: this.GamePlatform,
        StartDate: this.StartDate,
        EndDate: this.EndDate
      }
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
    formSubmit() {
      axios.post(
        `https://localhost:8080/api/brackets/${this.bracket.bracketID}/register/${this.gamer}`,
        {
          BracketName: this.BracketName,
          CompetitorCount: this.CompetitorCount,
          GamePlayed: this.GamePlayed,
          GamePlatform: this.GamePlatform,
          StartDate: this.StartDate,
          EndDate: this.EndDate
        }
      );
    }
  },
  computed: {
    ...authComputed
  }
};
