<template>
  <div class="event-create">
    <v-app id="inspire">
      <v-form
        ref="form"
        v-model="valid"
        @submit.prevent="createEvent"
      >
        <v-row justify="space-around">
          <v-col class="px-4" cols="12" sm="3">
            <v-text-field
              v-model="eventName"
              label="Event Name"
              :rules="eventNameRule"
              placeholder="Event Name of your choice"
              required
            >
            </v-text-field>

            <v-row>
              <v-col cols="12" md="15">
                <v-textarea
                v-model="eventDescription"
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
                        v-model="startDate"
                        label="Start Date"
                        :rules="[value => !!value || 'Required' ]"
                        v-on="on"
                        required
                      >
                      </v-text-field>
                    </template>
                    <v-date-picker
                      v-model="startDate"
                      @input="menu1 = false"
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
                        v-model="startTime"
                        label="Start Time"
                        :rules="[value => !!value || 'Required' ]"
                        v-on="on"
                        required
                      >
                      </v-text-field>
                    </template>
                  <v-time-picker
                    v-if="menu2"
                    v-model="startTime"
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
                        v-model="endDate"
                        label="End Date"
                        :rules="[value => !!value || 'Required' ]"
                        v-on="on"
                        required
                      >
                      </v-text-field>
                    </template>
                    <v-date-picker
                      v-model="endDate"
                      @input="menu3 = false"
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
                        v-model="endTime"
                        label="End Time"
                        :rules="[value => !!value || 'Required' ]"
                        v-on="on"
                        required
                      >
                      </v-text-field>
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
  </div>
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
    valid: true,
    topMenu:null,
    time:null,
    menu1:false,
    menu2:false,
    menu3:false,
    menu4:false,
    eventName:"",
    eventDescription:"",
    eventDescriptionRule: 
    [value => (value ||'').length <= 700 ||'max 700 characters'],
    startDate:null,
    startTime:null,
    endDate:null,
    endTime:null,
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
      this.$refs.form.validate()
      axios.post("https://localhost:44352/api/events/createEvent/${this.EventName}",
      {
        EventName: this.eventName,
        Address: this.eventAddress,
        Description: this.eventDescription,
        StartDate: this.startDate + this.startTime,
        EndDate: this.endDate + this.endTime,
        Host:this.$store.state.user.systemID
      }
      );
      // setTimeout((this.$store.dispatch('createEvent', this.EventInfo),500))
      this.$refs.form.reset();
    },
    countdown: function() {
      this.remainingCount = this.maxCount - this.eventDescription.length;
      this.hasError = this.remainingCount < 0;
    }
  },
}
</script>