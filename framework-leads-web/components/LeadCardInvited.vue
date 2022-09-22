<template>
  <div class="column">
    <div class="card">
      <header class="card-header">                
          <div class="name-initial">
            {{ nameInitial }}
          </div>
          <div class="rows">
          <span class="contact-name">{{ content.contactFirstName + ' ' +  content.contactLastName }}</span>
          <span class="date-created">{{ $moment(content.dateCreated).format('MMMM D @ HH:mm') }}</span>
          </div>
          
      </header>
      <div class="card-content">
        <div class="content columns content-first-section">        
          <span class="card-data card-address icon is-left">
            <font-awesome-icon :icon="['fas', 'location-dot']" /> <span>{{ content.suburb }}</span>            
          </span>
          <span class="card-data card-category icon is-left">
            <font-awesome-icon :icon="['fas', 'briefcase']" /> <span>{{ content.category }}</span>            
          </span>
          
          <span class="card-data">Job ID: {{ content.id }}</span>
        </div>
        <div>
          <span>{{ content.description }}</span>
        </div>
      </div>
      <footer class="card-footer">
        <div v-if="buttons" class="button-section">
          <b-button type="is-success" v-on:click="accept()">Accept</b-button>
          <b-button type="is-danger" v-on:click="reject()">Decline</b-button>
        </div>        
        <div>
          <span class="card-price"><strong>${{ content.price }}</strong>&nbsp;Lead Invitation</span>
        </div>
      </footer>
    </div>
  </div>
</template>

<style lang="scss">
  .name-initial {
    background-color: #7c3eaf;
    color: #fff;
    font-size: 2.25em;
    width: 60px;
    border-radius: 30px;
    height: 60px;
    text-align: center;
    padding-top: 2px;
    margin: 12px;
    margin-left: 20px;
  }

  .contact-name {
    padding-top: 4px;
    display: block;
    font-weight: 600;
    font-size: 2em;
    width: 100%;
  }

  .date-created {
    display: block;
    font-weight: 300;
    font-size: 0.9em;
    color: #888;
    width: 100%;
  }

  .content-first-section {
    border-bottom: 1px solid #ddd;
    padding-bottom: 10px;
  }  

  .card-data {
    display: block;    
    font-size: 1em;
    color: #888;
  }

  .card-address {
    min-width: 30%;
    padding-left: 12px;
  }

  .card-category {
    min-width: 15%;
  }

  .card-description {
    text-align: left;
    padding-left: 12px;
  }

  .button-section {
    padding: 10px;
    margin-left: 12px;    
    margin-right: 16px;
  }

  .card-price {
    display: inline-flex;
    height: 100%;
    align-items: center;
  }
</style>

<script>
export default {
  name: 'LeadCard',
  props: ['value', 'buttons'],
  computed: {
    nameInitial: function() {
      return this.$data.content.contactFirstName.substring(0, 1);
    }
  },
  data() {
    return {
      content: this.value
    }
  },
   methods: {
    accept() {
      this.$emit('accept', this.$data.content)
    },
    reject() {
      this.$emit('reject', this.$data.content)
    }

  }
}
</script>
