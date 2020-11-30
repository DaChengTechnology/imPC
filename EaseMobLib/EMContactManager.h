#pragma once
#include <emclient.h>
#include "EMError.h"
namespace EaseMobLib {
	public ref class EMContactListener : EMBase {
	public:
		/**
		* \brief callback function called when contact added.
		*
		* @param  username newly added contact name.
		*/
		delegate void D_onContactAdded(String^ username);

		/**
		* \brief called when contact deleted
		*
		* @param  usernames newly deleted contact name list.
		*/
		delegate void D_onContactDeleted(String^ username);

		/**
		* \brief  called when user be invited by contact to be friend.
		*
		* @param  username contact when invited.
		* @param  message contact sent, telling invitation reason.
		*/
		delegate void D_onContactInvited(String^ username, String^ reason);

		/**
		* \brief  called when user invite contact to be friend, and contact has accepted the invitation.
		*
		* @param  username contact whom accept invitation.
		*/
		delegate void D_onContactAgreed(String^ username);

		/**
		* \brief  called when user invite contact to be friend, and contact has declined the invitation.
		*
		* @param  username contact whom refused the invitation.
		*/
		delegate void D_onContactRefused(String^ username);
	public:
		D_onContactAdded^ onContactAdded = nullptr;
		D_onContactDeleted^ onContactDeleted = nullptr;
		D_onContactInvited^ onContactInvited = nullptr;
		D_onContactAgreed^ onContactAgreed = nullptr;
		D_onContactRefused^ onContactRefused = nullptr;
	};
	public ref class EMContactManager
	{
	public:
		/**
		* \brief constructor for EMContactManager,
		*
		* @param native contact manager interface pointer
		*/
		EMContactManager(easemob::EMContactManagerInterface*);
		~EMContactManager();

		/**
		* \brief register contact status change listener
		*
		* @param  listener contact status change listener
		*/
		void registerContactListener(EMContactListener^ listener);

		/**
		* \brief remove registration of contact status change listener
		*
		* @param  listener contact status change listener
		*/
		void removeContactListener(EMContactListener^ listener);

		/**
		* \brief retrieve current user's friend list from server.
		*
		* @return  contact list
		*/
		//cli::array<const String^>^ allContacts(EMError^ error);

		///**
		//* \brief retrieve current user's friend list from server.
		//*
		//* @return  contact list
		//*/
		//cli::array<const String^>^ getContactsFromServer(EMError^ error);

		///**
		//* \brief retrieve current user's friend list from local database.
		//*
		//* @return  contact list
		//*/
		//cli::array<const String^>^ getContactsFromDB(EMError^ error);

		/**
		* \brief invite contact to be friend, need contact accept.
		*
		* @param  username contact to be invited.
		* @param  message contact will receive the message when got invitation.
		*/
		void inviteContact(const String^ username, const String^ message, EMError^ error);

		/**
		* \brief delete contact from contact list.
		* contact part will auto be removed friend relationship.
		*
		* @param  username contact to be invited.
		*/
		void deleteContact(const String^ username, EMError^ error, bool keepConversation);

		/**
		* \brief accept contact's invitation
		*
		* @param  username contact who initiate invitation.
		*/
		void acceptInvitation(const String^ username, EMError^ error);

		/**
		* \brief decline contact's invitation
		*
		* @param  username contact who initiate invitation.
		*/
		void declineInvitation(const String^ username, EMError^ error);

		// -------------------------- blacklist --------------------------
		/**
		* \brief retrieve black list from memory
		*
		* @return  black list of current user, contacts in the block list can not send message or
		* inviation to user
		*/
		//cli::array<const String^>^ blacklist(EMError^ error);

		///**
		//* \brief retrieve black list from server
		//*
		//* Note: sync operation.
		//* returned result will also be updated in database.
		//* @return  black list of current user, contacts in the block list can not send message or
		//* inviation to user
		//*/
		//cli::array<const String^>^ getBlackListFromServer(EMError^ error);

		///**
		//* \brief retrieve black list from local database
		//*
		//* @return  black list of current user which stored in local database.
		//*/
		//cli::array<const String^>^ getBlackListFromDB(EMError^ error);

		/**
		* \brief save black list.
		*
		* Note: sync operation.
		* black list will be sent to server and local database will also updated.
		* @param  blacklist contacts in the block list can not send message
		* inviation to user
		*/
		void saveBlackList(cli::array<String^>^ blacklist, EMError^ error);

		/**
		* \brief add contact to blacklist
		*
		* Note: sync operation
		* new item will updated to remote server, and also update local database.
		* @param  username contact to be added to blacklist
		* @param  both whether both side will be blocked, if true user also can not subscribe contact's presense. both = false is not work yet, current behaviour is both side conmunication will be blocded.
		*/
		void addToBlackList(const String^ username, bool both, EMError^ error);

		/**
		* \brief remove contact from black list
		*
		* Note: sync operation
		* new item will updated to remote server, and also update local database.
		* @param  username contact to be removed from blacklist
		*/
		void removeFromBlackList(const String^ username, EMError^ error);

	private:
		void* listenerDelegate;
		easemob::EMContactManagerInterface* getImpl();
		easemob::EMContactManagerInterface* mImpl;
	};
}

