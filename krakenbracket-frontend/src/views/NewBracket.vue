<template>
    <div class="new-bracket">
        <v-app id="inspire">
        <div v-if="!loggedIn">
          <NotLoggedIn />
        </div>
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
                                ref="menu"
                                v-model="StartDate"
                                :close-on-content-click="false"
                                :return-value.sync="date"
                                transition="scale-transition"
                                offset-y
                                min-width="290px"
                            >
                                <template v-slot:activator="{ on }">
                                <v-text-field
                                    v-model="date"
                                    label="Start Date"
                                    readonly
                                    v-on="on"
                                ></v-text-field>
                                </template>
                                <v-date-picker v-model="date" no-title scrollable>
                                <v-spacer></v-spacer>
                                <v-btn text color="primary" @click="menu = false">Cancel</v-btn>
                                <v-btn text color="primary" @click="$refs.menu.save(date)">OK</v-btn>
                                </v-date-picker>
                            </v-menu>
                        </v-col>
                        <v-col cols="12" lg="6">
                            <v-menu
                                ref="menu"
                                v-model="EndDate"
                                :close-on-content-click="false"
                                :return-value.sync="date"
                                transition="scale-transition"
                                offset-y
                                min-width="290px"
                            >
                                <template v-slot:activator="{ on }">
                                <v-text-field
                                    v-model="date"
                                    label="End Date"
                                    readonly
                                    v-on="on"
                                ></v-text-field>
                                </template>
                                <v-date-picker v-model="date" no-title scrollable>
                                <v-spacer></v-spacer>
                                <v-btn text color="primary" @click="menu = false">Cancel</v-btn>
                                <v-btn text color="primary" @click="$refs.menu.save(date)">OK</v-btn>
                                </v-date-picker>
                            </v-menu>
                        </v-col>
                    </v-row>
                </v-container>

                <v-btn x-large>confirm</v-btn>
                
                </v-col>
        </v-row>
        </v-app>
    </div>
</template>

<script>
import NotLoggedIn from "../components/NotLoggedIn.vue";
export default {
    props: ["id"],
    components: {
    NotLoggedIn
  },
  data: () => ({
      dropdown_gamePlayed:['Street Fighter V - Arcade Edition', 'The King of Fighters XIV',
      'Tekken 7', 'Super Smash Bros. Ultimate', 'Samurai Shodown', "Soul Calibur VI", 
      'Mortal Kombat 11', 'Injustice 2', 'Killer Instinct'],
      dropdown_gamePlatform: ['Playstation 3', 'Playstation 4', 'Xbox 360', 'Xbox One', 'Wii', 'Wii U', "Switch"],
      date: new Date().toISOString().substr(0, 10),
    menu: false,
    modal: false,
    menu2: false
    }),
    methods: {
        Submit() {
        }

    },
  }
</script>