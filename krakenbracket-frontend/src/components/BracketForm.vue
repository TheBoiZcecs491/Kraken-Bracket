<template>
    <div class="new-bracket">
        <v-app id="inspire">
        <h1>Create a new bracket</h1>
        <v-form
                ref="form"
                v-model="valid"
        >
        <v-row justify="space-around">
            
                <v-col class="px-4" cols="12" sm="3">
                <v-text-field
                    v-model="BracketName"
                    label="Bracket Name"
                    :rules="bracketNameRules"
                    placeholder="EVO 2020 SFVAE Pools - 1"
                    required
                ></v-text-field>

                <v-slider
                    v-model="CompetitorCount"
                    min="2"
                    max="128"
                    label="Competitors"
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
                        v-model="GamePlayed"
                        class="my-2"
                        :items="dropdown_gamePlayed"
                        :editable= true
                        :menu-props="topMenu ? 'top' : ''"
                        label="Game played"
                        :rules="gamePlayedRules"
                        target="#dropdown_gamePlayed"
                        required
                    ></v-overflow-btn>
                </v-container>
                <v-container id="GamePlatform">
                    <v-overflow-btn
                        v-model="GamePlatform"
                        class="my-2"
                        :items="dropdown_gamePlatform"
                        :editable= true
                        :menu-props="topMenu ? 'top' : ''"
                        label="Platform"
                        target="#dropdown_gamePlatform"
                        :rules="platformRules"
                        required
                    ></v-overflow-btn>
                </v-container>

                <v-row>
                    <v-col cols="12" md="15">
                        <v-textarea
                        v-model="ruleSet"
                        :rules="rulesMaxChars"
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
                                v-model="menu1"
                                :close-on-content-click="false"
                                :nudge-left="-40"
                                transition="scale-transition"
                                offset-y
                                min-width="290px"
                            >
                                <template v-slot:activator="{ on }">
                                <v-text-field
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
                                 @input="menu1 = false"
                                 ></v-date-picker>
                            </v-menu>
                        </v-col>
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
                                    v-model="startTime"
                                    label="Start Time"
                                    :rules="[value => !!value || 'Required']"
                                    v-on="on"
                                    required
                                ></v-text-field>
                                </template>
                                <v-time-picker
                                v-if="menu2"
                                v-model="startTime"
                                full-width
                                ></v-time-picker>
                            </v-menu>
                        </v-col>
                    </v-row>
                </v-container>
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
                                    v-model="endDate"
                                    label="End Date"
                                    :rules="[value => !!value || 'Required']"
                                    readonly
                                    v-on="on"
                                    required
                                ></v-text-field>
                                </template>
                                <v-date-picker v-model="endDate" @input="menu3 = false"></v-date-picker>
                            </v-menu>
                        </v-col>
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
                                ></v-time-picker>
                            </v-menu>
                        </v-col>
                    </v-row>
                </v-container>
                <v-btn
                    :disabled="!valid"
                    x-large
                    @click="Submit"
                    >
                    Create Bracket
                </v-btn>
                </v-col>
        </v-row>
        </v-form>
        </v-app>
    </div>
</template>

<script>
export default {
    props: ["id"],
    components: {
  },
  data: () => ({
    dropdown_gamePlayed:['Street Fighter V - Arcade Edition', 'The King of Fighters XIV',
    'Tekken 7', 'Super Smash Bros. Ultimate', 'Samurai Shodown', "Soul Calibur VI", 
    'Mortal Kombat 11', 'Injustice 2', 'Killer Instinct'],
    dropdown_gamePlatform: ['Playstation 3', 'Playstation 4', 'Xbox 360', 'Xbox One', 'Wii', 'Wii U', "Switch"],
    currentDate: new Date().toISOString().substr(0, 10),
    valid: true,
    topMenu: null,
    time: null,
    menu1: false,
    menu2: false,
    menu3: false,
    menu4: false,
    BracketName: "",
    CompetitorCount: "",
    GamePlayed: "",
    GamePlatform: "",
    ruleSet: "",
    startDate: null,
    startTime: null,
    endDate: null,
    endTime: null,
    rulesMaxChars: 
    [value => (value || '').length <= 700 || 'Max 700 characters',],
    bracketNameRules: 
    [value => !!value || 'Bracket name required',
    value => (value || '').length >= 5 || 'Min 5 characters', 
    value => (value || '').length <= 75 || 'Max 75 characters'],
    gamePlayedRules:
    [value => !!value || 'Game required'],
    platformRules:
    [value => !!value || 'Platform required'],
    confirmRules:
    [value => !!value || 'Platform required']
    }),
    methods: {
        Submit() {
            this.$refs.form.validate()
            const bracketInfo = {
                BracketName: this.BracketName,
                CompetitorCount: this.CompetitorCount,
                GamePlayed: this.GamePlayed,
                GamePlatform: this.GamePlatform,
                Rules: this.ruleSet,
                StartDate: this.startDate,
                StartTime: this.startTime,
                EndDate: this.endDate,
                EndTime: this.endTime 
            }
            console.log(bracketInfo)
            this.$refs.form.reset()
        },
    },
  }
</script>