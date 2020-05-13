<template>
  <!-- <div class="event-create"> -->
  <v-app id="inspire">
    <v-form ref="form" v-model="valid" @submit.prevent="createEvent">
      <v-row justify="space-around">
        <v-col class="px-4" cols="12" sm="3">
          <v-text-field
            v-model="EventName"
            label="Event Name"
            :rules="eventNameRule"
            placeholder="Kraken Bracket Championship"
            required
            class="EventName-input"
          >
          </v-text-field>

          <v-text-field
            v-model="EventAddress"
            label="Event Address"
            :rules="eventAddressRule"
            placeholder="Ex. 1234 Street Name"
            required
            :hint="remainingAddressCount.toString()"
            v-on:keyup="countdownAddress"
            class="EventAddress-input"
          >
          </v-text-field>

          <v-row>
            <v-col cols="12" md="15">
              <v-textarea
                v-model="EventDescription"
                label="Event Description"
                :rules="eventDescriptionRule"
                :placeholder="
                  'Quick description of the Event \n (700 char max)'
                "
                :hint="remainingDescriptionCount.toString()"
                v-on:keyup="countdownDescription"
                class="EventDescription-input"
              >
              </v-textarea>
            </v-col>
          </v-row>

          <v-container>
            <v-row>
              <v-col cols="12" lg="6">
                <v-menu
                  v-model="menuStartDate"
                  :close-on-content-click="false"
                  :nudge-left="-40"
                  translate="scale-transition"
                  offset-y
                  min-width="290px"
                >
                  <template v-slot:activator="{ on }">
                    <v-text-field
                      v-model="StartDate"
                      label="Start Date"
                      :rules="[value => !!value || 'Required']"
                      v-on="on"
                      required
                      readonly
                      class="StartDate-input"
                    >
                    </v-text-field>
                  </template>
                  <v-date-picker
                    v-model="StartDate"
                    @input="menuStartDate = false"
                    :min="currentDate"
                    :max="EndDate"
                  >
                  </v-date-picker>
                </v-menu>
              </v-col>

              <v-spacer></v-spacer>

              <v-col cols="12" lg="6">
                <v-menu
                  ref="menuStartTime"
                  v-model="menuStartTime"
                  :close-on-content-click="false"
                  :nudge-left="40"
                  :return-value.sync="time"
                  translate="scale-transition"
                  offset-y
                  min-width="290px"
                  max-width="290px"
                >
                  <template v-slot:activator="{ on }">
                    <v-text-field
                      v-model="StartTime"
                      label="Start Time"
                      :rules="[value => !!value || 'Required']"
                      v-on="on"
                      required
                      readonly
                    >
                    </v-text-field>
                  </template>
                  <v-time-picker
                    v-if="menuStartTime"
                    v-model="StartTime"
                    full-width
                    ampm-in-title
                  >
                  </v-time-picker>
                </v-menu>
              </v-col>
            </v-row>
          </v-container>

          <v-container>
            <v-row>
              <v-col cols="12" lg="6">
                <v-menu
                  v-model="menuEndDate"
                  :close-on-content-click="false"
                  :nudge-left="-40"
                  translate="scale-transition"
                  offset-y
                  min-width="290px"
                >
                  <template v-slot:activator="{ on }">
                    <v-text-field
                      v-model="EndDate"
                      label="End Date"
                      :rules="[value => !!value || 'Required']"
                      v-on="on"
                      required
                      readonly
                    >
                    </v-text-field>
                  </template>
                  <v-date-picker
                    v-model="EndDate"
                    @input="menuEndDate = false"
                    :min="StartDate"
                  >
                  </v-date-picker>
                </v-menu>
              </v-col>

              <v-spacer></v-spacer>

              <v-col cols="12" lg="6">
                <v-menu
                  ref="menuEndTime"
                  v-model="menuEndTime"
                  :close-on-content-click="false"
                  :nudge-left="40"
                  :return-value.sync="time"
                  translate="scale-transition"
                  offset-y
                  min-width="290px"
                  max-width="290px"
                >
                  <template v-slot:activator="{ on }">
                    <v-text-field
                      v-model="EndTime"
                      label="End Time"
                      :rules="[value => !!value || 'Required']"
                      v-on="on"
                      required
                      readonly
                    >
                    </v-text-field>
                  </template>
                  <v-time-picker
                    v-if="menuEndTime"
                    v-model="EndTime"
                    ampm-in-title
                    full-width
                  ></v-time-picker>
                </v-menu>
              </v-col>
            </v-row>
          </v-container>
          <div v-if="statusHost()">
            <v-btn :disable="!valid" x-large @click="SubmitUpdate">
            Update Event
            </v-btn>
          </div>
          <div v-else>
            <v-btn :disable="!valid" x-large @click="SubmitCreate">
            Create Event
            </v-btn>
          </div>
          <v-btn @click="test">
            CLICKL
          </v-btn>
          <!-- <v-btn-if=this.$store.user.systemID
              :disable="!valid"
              x-large
              @click="SubmitUpdate"
            >
            Create Event
            </v-btn> -->
        </v-col>
      </v-row>
    </v-form>
  </v-app>
  <!-- </div> -->
</template>

<script>
import axios from "axios";
import { authComputed } from "../store/helpers.js";

export default {
  props: {
    event: Object
  },
  components: {},
  computed: {
    ...authComputed
  },

  data: () => ({
    currentDate: new Date().toISOString().substr(0, 10),
    Host: "",
    valid: true,
    topMenu: null,
    time: null,
    menuStartDate: false,
    menuStartTime: false,
    menuEndDate: false,
    menuEndTime: false,

    EventName: "",
    eventNameRule: [
      value => !!value || "Event name required",
      value => (value || "").length >= 5 || "Min 5 characters",
      value => (value || "").length <= 75 || "Max 75 characters"
    ],

    EventAddress: "",
    EventAddressRule: [
      value => !!value || "Event Address required",
      value => (value || "").length >= 5 || "Min 5 characters",
      value => (value || "").length <= 75 || "Max 75 characters"
    ],

    EventDescription: "",
    eventDescriptionRule: [
      value => (value || "").length <= 700 || "max 700 characters"
    ],
    StartDate: null,
    StartTime: null,
    EndDate: null,
    EndTime: null,

    maxNameCount: 75,
    remainingNameCount: 75,

    maxAddressCount: 75,
    remainingAddressCount: 75,

    maxDescriptionCount: 700,
    remainingDescriptionCount: 700,

    hasError: false
  }),
  methods: {
    setData(){
    this.EventName = this.$route.params.event.eventName
    },
    test(){
      console.log("EVNET ID: " + this.event)
    },
    mounted(){
      try{
        this.EventName = this.$route.params.event.eventName
      }
      catch(err){
        throw false;
      }
    },
    statusHost() {
      try{
        if (this.$store.state.gamerInfo.gamerTag == this.$route.params.event.host) {
          return true;
        } else {
          return false;
        }
      }
      catch(err){
        return false;
      }
     
    },
    SubmitUpdate() {
      console.log(this.event.eventID)
      axios.put(`https://localhost:44352/api/events/updateEvent`, {
        EventID: this.event.eventID,
         EventName: this.EventName,
          Address: this.EventAddress,
          Description: this.EventDescription,
          StartDate: this.StartDate + " " + this.StartTime,
          EndDate: this.EndDate + " " + this.EndTime,
          StatusCode: 1,
          Host: this.$store.state.gamerInfo.hashedUserID
      });
      this.$refs.form.reset()
    },
    SubmitCreate() {
      // this.$refs.form.validate()
      let res = axios
        .post(`https://localhost:44352/api/events/createEvent`, {
          EventName: this.EventName,
          Address: this.EventAddress,
          Description: this.EventDescription,
          StartDate: this.StartDate + " " + this.StartTime,
          EndDate: this.EndDate + " " + this.EndTime,
          StatusCode: 1,
          Host: this.$store.state.gamerInfo.hashedUserID
        })
        .then(function(response) {
          console.log(response);
        });
      console.log(`Data: ${res.data}`);
      this.$refs.form.reset()
      // .then(
      // this.$router.go(-1)
      // )
      // setTimeout((this.$store.dispatch('createEvent', this.EventInfo),500))
    },

    countdownDescription: function() {
      this.remainingDescriptionCount =
        this.maxDescriptionCount - this.EventDescription.length;
      this.hasError = this.remainingDescriptionCount < 0;
    },

    countdownAddress: function() {
      this.remainingAddressCount =
        this.maxAddressCount - this.EventAddress.length;
      this.hasError = this.remainingAddressCount < 0;
    }
  }
};
</script>
