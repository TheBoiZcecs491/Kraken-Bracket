<template>
  <v-app id="inspire">
    <!-- Checking for completed fields to enable "Create Bracket" button -->
    <v-form ref="form" v-model="valid" @submit.prevent="createBracket">
      <h1>Update Bracket</h1>
      <v-row justify="space-around">
        <!-- Bracket name field -->
        <v-col class="px-4" cols="12" sm="3">
          <v-text-field
            class="BracketName-input"
            v-model="BracketName"
            label="Bracket Name"
            :rules="bracketNameRules"
            placeholder="EVO 2020 SFVAE Pools - 1"
            required
          ></v-text-field>

          <!-- Player count slider -->
          <v-slider
            v-model="MaxCapacity"
            min="2"
            max="128"
            label="Competitor Cap"
            readonly
            ><template v-slot:append>
              <v-text-field
                class="PlayerCount-input"
                v-model="MaxCapacity"
                hide-details
                single-line
                type="number"
                style="width: 60px"
                readonly
              ></v-text-field>
            </template>
          </v-slider>

          <!-- Player count radio buttons -->
          <v-container fluid>
            <v-radio-group v-model="MaxCapacity" row>
              <v-radio label="4" value="4"></v-radio>
              <v-radio label="8" value="8"></v-radio>
              <v-radio label="16" value="16"></v-radio>
              <v-radio label="32" value="32"></v-radio>
              <v-radio label="64" value="64"></v-radio>
              <v-radio label="128" value="128"></v-radio>
            </v-radio-group>
          </v-container>

          <!-- Game played drop down bar (Fillable) -->
          <v-container id="GamePlayed">
            <v-overflow-btn
              v-model="GamePlayed"
              class="GamePlayed-input"
              :items="dropdown_gamePlayed"
              :editable="true"
              :menu-props="topMenu ? 'top' : ''"
              label="Game played"
              :rules="gamePlayedRules"
              target="#dropdown_gamePlayed"
              required
            ></v-overflow-btn>
          </v-container>
          <!-- Gaming platform drop down bar (fillable) -->
          <v-container id="GamingPlatform">
            <v-overflow-btn
              v-model="GamingPlatform"
              class="GamingPlatform-input"
              :items="dropdown_gamingPlatform"
              :editable="true"
              :menu-props="topMenu ? 'top' : ''"
              label="Platform"
              target="#dropdown_gamingPlatform"
              :rules="platformRules"
              required
            ></v-overflow-btn>
          </v-container>
          <!-- Rule set text area (scalable) -->
          <v-row>
            <v-col cols="12" md="15">
              <v-textarea
                class="ruleSet-input"
                v-model="ruleSet"
                :rules="rulesMaxChars"
                name="Rules"
                label="Rule set"
                hint="700 char max"
              ></v-textarea>
            </v-col>
          </v-row>
          <!-- Start date picker -->
          <v-container>
            <v-row>
              <v-col cols="12" lg="6">
                <v-menu
                  v-model="menu1"
                  :close-on-content-click="false"
                  :nudge-left="-40"
                  transition="scale-transition"
                  offset-y
                  min-width="290px"
                >
                  <template v-slot:activator="{ on }">
                    <v-text-field
                      class="startDate-input"
                      v-model="startDate"
                      label="Start Date"
                      :rules="[value => !!value || 'Required']"
                      readonly
                      v-on="on"
                      required
                    ></v-text-field>
                  </template>
                  <v-date-picker
                    v-model="startDate"
                    :max="endDate"
                    @input="menu1 = false"
                  ></v-date-picker>
                </v-menu>
              </v-col>
              <!-- Start time picker -->
              <v-spacer></v-spacer>
              <v-col cols="12" lg="6">
                <v-menu
                  ref="menu2"
                  v-model="menu2"
                  :close-on-content-click="false"
                  :nudge-right="40"
                  :return-value.sync="time"
                  transition="scale-transition"
                  offset-y
                  max-width="290px"
                  min-width="290px"
                >
                  <template v-slot:activator="{ on }">
                    <v-text-field
                      class="startTime-input"
                      v-model="startTime"
                      label="Start Time"
                      :rules="[value => !!value || 'Required']"
                      readonly
                      v-on="on"
                      required
                    ></v-text-field>
                  </template>
                  <v-time-picker
                    v-if="menu2"
                    v-model="startTime"
                    full-width
                    scrollable
                    ampm-in-title
                  ></v-time-picker>
                </v-menu>
              </v-col>
            </v-row>
          </v-container>
          <!-- End date picker -->
          <v-container>
            <v-row>
              <v-col cols="12" lg="6">
                <v-menu
                  v-model="menu3"
                  :close-on-content-click="false"
                  :nudge-right="40"
                  transition="scale-transition"
                  offset-y
                  min-width="290px"
                >
                  <template v-slot:activator="{ on }">
                    <v-text-field
                      class="endDate-input"
                      v-model="endDate"
                      label="End Date"
                      :rules="[value => !!value || 'Required']"
                      readonly
                      v-on="on"
                      required
                    ></v-text-field>
                  </template>
                  <v-date-picker
                    v-model="endDate"
                    :min="startDate"
                    @input="menu3 = false"
                  ></v-date-picker>
                </v-menu>
              </v-col>
              <!-- End time picker -->
              <v-spacer></v-spacer>
              <v-col cols="12" lg="6">
                <v-menu
                  ref="menu4"
                  v-model="menu4"
                  :close-on-content-click="false"
                  :nudge-right="40"
                  :return-value.sync="time"
                  transition="scale-transition"
                  offset-y
                  max-width="290px"
                  min-width="290px"
                >
                  <template v-slot:activator="{ on }">
                    <v-text-field
                      class="endTime-input"
                      v-model="endTime"
                      label="End Time"
                      :rules="[value => !!value || 'Required']"
                      readonly
                      v-on="on"
                      required
                    ></v-text-field>
                  </template>
                  <v-time-picker
                    v-if="menu4"
                    v-model="endTime"
                    full-width
                    scrollable
                    ampm-in-title
                  ></v-time-picker>
                </v-menu>
              </v-col>
            </v-row>
          </v-container>
          <v-btn :disabled="!valid" class="primary" x-large @click="Submit">
            Update Bracket
          </v-btn>
        </v-col>
      </v-row>
    </v-form>
  </v-app>
</template>

<script>
import axios from "axios";
export default {
  props: ["id"],
  components: {},
  data: () => ({
    dropdown_gamePlayed: [
      "Street Fighter V - Arcade Edition",
      "The King of Fighters XIV",
      "Tekken 7",
      "Super Smash Bros. Ultimate",
      "Samurai Shodown",
      "Soul Calibur VI",
      "Mortal Kombat 11",
      "Injustice 2",
      "Killer Instinct"
    ],
    dropdown_gamingPlatform: [
      "Playstation 3",
      "Playstation 4",
      "Xbox 360",
      "Xbox One",
      "Wii",
      "Wii U",
      "Switch"
    ],
    currentDate: new Date().toISOString().substr(0, 10),
    valid: true,
    topMenu: null,
    time: null,
    menu1: false,
    menu2: false,
    menu3: false,
    menu4: false,
    BracketName: "",
    Host: "",
    MaxCapacity: "",
    GamePlayed: "",
    GamingPlatform: "",
    ruleSet: "",
    startDate: null,
    startTime: null,
    endDate: null,
    endTime: null,
    rulesMaxChars: [
      value => (value || "").length < 700 || "Max 700 characters"
    ],
    bracketNameRules: [
      value => !!value || "Bracket name required",
      value => (value || "").length > 5 || "Min 5 characters",
      value => (value || "").length < 75 || "Max 75 characters"
    ],
    gamePlayedRules: [value => !!value || "Game required"],
    platformRules: [value => !!value || "Platform required"]
  }),
  methods: {
    Submit() {
      this.$refs.form.validate();
      axios.put(`https://localhost:44352/api/brackets/updateBracket/`, {
        bracketID: this.id,
        BracketName: this.BracketName,
        Host: this.$store.state.gamerInfo.gamerTag,
        PlayerCount: "0",
        MaxCapacity: this.MaxCapacity,
        GamePlayed: this.GamePlayed,
        GamingPlatform: this.GamingPlatform,
        Rules: this.ruleSet,
        StartDate: this.startDate + " " + this.startTime, // Datetime concatenate
        EndDate: this.endDate + " " + this.endTime, // Datetime concatenate
        Reason: ""
      });
      //console.log(bracketInfo)
      //setTimeout((this.$store.dispatch('createBracket', this.BracketInfo), 500))
      this.$refs.form.reset();
    }
  }
};
</script>

<style>
.create-btn {
  text-decoration: none;
}
</style>
