<template>
  <div class="hello">
    <v-app id="inspire">    
      <v-row justify="space-around">
            <v-col class="px-4" cols="12" sm="3">
              <v-text-field
                v-model="BracketName"
                label="Bracket Name"
                placeholder="EVO 2020 SFVAE Pools - 1"
                outlined
              ></v-text-field>

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

              <v-text-field
                v-model="age"
                label="Age"
                placeholder="年齢"
                outlined
              ></v-text-field>

              <v-text-field
                v-model="location"
                label="Location"
                placeholder="場所"
                outlined
              ></v-text-field>

              <v-text-field
                v-model="docID"
                label="Document ID"
                placeholder="ドキュメント"
                outlined
              ></v-text-field>
            </v-col>
      </v-row>
    </v-app>
  </div>
</template>

<script>
export default {
  name: "BracketForm",
  props: {
    msg: String
  }
};
</script>