<template>
    <div class="new-bracket">
        <v-app id="inspire">
        <h1>Create a new bracket</h1>
        <v-row justify="space-around">
                <v-col class="px-4" cols="12" sm="3">
                <v-text-field
                    v-model="BracketName"
                    label="Bracket Name"
                    placeholder="EVO 2020 SFVAE Pools - 1"
                ></v-text-field>

                <v-container id="dropdown">
                    <v-overflow-btn
                        class="my-4"
                        :items="dropdown_bracketType"
                        label="Bracket Type"
                        target="#dropdown"
                    ></v-overflow-btn>
                </v-container>

                <v-slider
                v-model="slider"
                class="align-center"
                :max=128
                :min=2
                hide-details
                >
                <template v-slot:append>
                    <v-text-field
                    v-model="slider"
                    class="mt-0 pt-0"
                    hide-details
                    single-line
                    type="number"
                    style="width: 60px"
                    ></v-text-field>
                </template>
                </v-slider>

                <v-container id="dropdown-playground">
                    <v-overflow-btn
                        class="my-2"
                        :items="dropdown_gamePlayed"
                        :editable= true
                        :segmented="segmented"
                        :loading="loading"
                        :disabled="disabled"
                        :readonly="readonly"
                        :filled="filled"
                        :reverse="reverse"
                        :dense="dense"
                        :persistent-hint="persistentHint"
                        :menu-props="topMenu ? 'top' : ''"
                        hint="Fill out if game not found"
                        label="Game played"
                        target="#dropdown_gamePlayed"
                    ></v-overflow-btn>
                    <v-overflow-btn
                        class="my-2"
                        :items="dropdown_gamePlatform"
                        :editable= true
                        :segmented="segmented"
                        :loading="loading"
                        :disabled="disabled"
                        :readonly="readonly"
                        :filled="filled"
                        :reverse="reverse"
                        :dense="dense"
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
                        name="input-7-1"
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
                            v-model="menu1"
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
                            v-model="menu1"
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
        </v-app>
    </div>
</template>

<script>
  export default {
    data: () => ({
      dropdown_bracketType: ['Single Elimination', 'Double Elimination', 'Round Robin'],
      dropdown_gamePlayed:['Street Fighter V - Arcade Edition', 'The King of Fighters XIV',
      'Tekken 7', 'Super Smash Bros. Ultimate', 'Samurai Shodown', "Soul Calibur VI", 
      'Mortal Kombat 11', 'Injustice 2', 'Killer Instinct'],
      dropdown_gamePlatform: ['Playstation 3', 'Playstation 4', 'Xbox 360', 'Xbox One', 'Wii', 'Wii U', "Switch"]
    }),
  }
</script>