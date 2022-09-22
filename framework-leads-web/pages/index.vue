<template>
  <section class="section">
    
    <b-tabs v-model="activeTab">
      <b-tab-item label="Invited">
        <div v-for="(item, index) in items" :key="item.id">
          <lead-card-invited :buttons="true" @accept="confirmAcceptLead" @reject="confirmRejectLead" v-if="items[index].status == 0" v-model="items[index]" />        
        </div>
      </b-tab-item>

      <b-tab-item label="Accepted">
        <div v-for="(item, index) in items" :key="item.id">
          <lead-card-accepted :buttons="false" @accept="confirmAcceptLead" @reject="confirmRejectLead" v-if="items[index].status == 1" v-model="items[index]" />        
        </div>
      </b-tab-item>            
  </b-tabs>

  <b-modal v-model="isAcceptModalActive">      
    <div class="content has-text-centered">
      
    </div>  
  </b-modal>

  </section>
</template>

<script>
import LeadCardInvited from '~/components/LeadCardInvited'
import LeadCardAccepted from '~/components/LeadCardAccepted'

export default {
  name: 'IndexPage',
  components: {
    LeadCardInvited,
    LeadCardAccepted
  },
  data () {
    return {
      items: [],
      activeTab: 0,
      isAcceptModalActive: false,
      currentLead: {}
    }
  },
  async mounted() {
    await this.fetchData()
  },
  methods: {
    async fetchData() {
      this.items = await this.$axios.$get(`/Lead`)
    },
    async updatePage() {
      await this.fetchData()
    },
    async confirmAcceptLead(lead) {
      
      var dialogMessage = `Are you sure you want to accept lead for ${lead.contactFirstName} ${lead.contactLastName}, with a price of $${lead.price}?`
      if (lead.price > 500) {
        dialogMessage += ' (a 10% discount will be applied)'
      }
      this.$buefy.dialog.confirm({
          message: dialogMessage,
          onConfirm: async () => {
            await this.$axios.$put(`/Lead/${lead.id}/accept`)
            await this.fetchData()
            this.$buefy.toast.open('Lead accepted successfully')
          }
      })
    },

    async confirmRejectLead(lead) {
      
      var dialogMessage = `Are you sure you want to decline lead for ${lead.contactFirstName} ${lead.contactLastName}?`
      
      this.$buefy.dialog.confirm({
          message: dialogMessage,
          onConfirm: async () => {
            await this.$axios.$put(`/Lead/${lead.id}/reject`)
            await this.fetchData()
            this.$buefy.toast.open('Lead declined successfully')
          }
      })
    },
  }
}
</script>
