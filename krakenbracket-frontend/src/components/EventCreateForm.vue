<template>
  <!-- <div class="event-create"> -->
    <v-app id="inspire">
      <v-form
        ref="form"
        v-model="valid"
        @submit.prevent="createEvent"
      >
        <v-row justify="space-around">
          <v-col class="px-4" cols="12" sm="3">
            <v-text-field
              v-model="EventName"
              label="Event Name"
              :rules="eventNameRule"
              placeholder="Event Name of your choice"
              required
            >
            </v-text-field>

            <v-row>
              <v-col cols="12" md="15">
                <v-textarea
                v-model="EventDescription"
                label="Event Description"
                :rules="eventDescriptionRule"
                :placeholder="'Quick description of the Event \n (700 char max)'"
                :hint= remainingCount.toString()
                v-on:keyup="countdown"
                >
                </v-textarea>
              </v-col>
            </v-row>

            <v-container>
              <v-row>
                <v-col cols="12" lg="6">
                  <v-menu
                    v-model="menu1"
                    :close-on-content-click="false"
                    :nudge-left="-40"
                    translate="scale-transition"
                    offset-y 
                    min-width="290px"
                  >
                    <template v-slot:activator="{on}">
                      <v-text-field
                        v-model="StartDate"
                        label="Start Date"
                        :rules="[value => !!value || 'Required' ]"
                        v-on="on"
                        required
                        readonly
                      >
                      </v-text-field>
                    </template>
                    <v-date-picker
                      v-model="StartDate"
                      @input="menu1 = false"
                      :min="currentDate"
                      :max="EndDate"
                    >
                    </v-date-picker>
                  </v-menu>
                </v-col>

                <v-spacer></v-spacer>
                
                <v-col cols="12" lg="6">
                  <v-menu
                    ref="menu2"
                    v-model="menu2"
                    :close-on-content-click="false"
                    :nudge-left="40"
                    :return-value.sync="time"
                    translate="scale-transition"
                    offset-y
                    min-width="290px"
                    max-width="290px"
                  >
                    <template v-slot:activator="{on}">
                      <v-text-field
                        v-model="StartTime"
                        label="Start Time"
                        :rules="[value => !!value || 'Required' ]"
                        v-on="on"
                        required
                        readonly
                      >
                      </v-text-field>
                    </template>
                  <v-time-picker
                    v-if="menu2"
                    v-model="StartTime"
                    full-width
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
                    v-model="menu3"
                    :close-on-content-click="false"
                    :nudge-left="-40"
                    translate="scale-transition"
                    offset-y
                    min-width="290px"
                  >
                    <template v-slot:activator="{on}">
                      <v-text-field
                        v-model="EndDate"
                        label="End Date"
                        :rules="[value => !!value || 'Required' ]"
                        v-on="on"
                        required
                        readonly
                      >
                      </v-text-field>
                    </template>
                    <v-date-picker
                      v-model="EndDate"
                      @input="menu3 = false"
                      :min="StartDate"
                    >
                    </v-date-picker>
                  </v-menu>
                </v-col>

                <v-spacer></v-spacer>

                <v-col cols="12" lg="6">
                    <v-menu
                      ref="menu4"
                      v-model="menu4"
                      :close-on-content-click="false"
                      :nudge-left="40"
                      :return-value.sync="time"
                      translate="scale-transition"
                      offset-y
                      min-width="290px"
                      max-width="290px"
                    >
                    <template v-slot:activator="{on}">
                      <v-text-field
                        v-model="EndTime"
                        label="End Time"
                        :rules="[value => !!value || 'Required' ]"
                        v-on="on"
                        required
                        readonly
                      >
                      </v-text-field>
                    </template>
                    <v-time-picker
                      v-if="menu4"
                      v-model="EndTime"
                      full-width
                    ></v-time-picker>
                  </v-menu>
                </v-col>
              </v-row>
            </v-container>
            <v-btn
              :disable="!valid"
              x-large
              @click="Submit"
            >
            Create Event
            </v-btn>
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
  props: ["id"],
  components:{},
  computed: {
    ...authComputed
  },
  data:() =>({
    currentDate: new Date().toISOString().substr(0,10),
    EventAddress: "1111",
    Host:"1111",
    valid: true,
    topMenu:null,
    time:null,
    menu1:false,
    menu2:false,
    menu3:false,
    menu4:false,
    EventName:"1111",
    EventDescription:"1111",
    eventDescriptionRule: 
    [value => (value ||'').length <= 700 ||'max 700 characters'],
    StartDate:null,
    StartTime:null,
    EndDate:null,
    EndTime:null,
    eventNameRule:  
    [value => !!value || 'Event name required',
    value => (value || '').length >= 5 || 'Min 5 characters', 
    value => (value || '').length <= 75 || 'Max 75 characters'],
    maxCount: 700,
    remainingCount: 700,
    hasError: false
  }),
  methods: {
    Submit(){
      // this.$refs.form.validate()
      axios.post(`https://localhost:44352/api/events/createEvent/${this.EventName}`,{
        EventName: this.EventName,
        Address: this.EventAddress,
        Description: this.EventDescription,
        StartDate: this.StartDate + " " + this.StartTime,
        EndDate: this.EndDate + " " + this.EndTime
        // Host:this.$store.state.user.systemID.toISOString
      }
      );
      // setTimeout((this.$store.dispatch('createEvent', this.EventInfo),500))
    },
    countdown: function() {
      this.remainingCount = this.maxCount - this.EventDescription.length;
      this.hasError = this.remainingCount < 0;
    }
  },
}
</script>