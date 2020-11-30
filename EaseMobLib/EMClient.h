#pragma once
#include <emclient.h>
#include "emsbsase.h"
#include "EMError.h"
#include "EMLoginInfo.h"
#include "EMChatConfigs.h"
#include "EMContactManager.h"
#include "EMConversation.h"
#include "EMGroupManager.h"
#include "EMChatManager.h"

namespace EaseMobLib {
	public enum class EMNetworkType
	{
		NONE,
		CABLE,
		WIFI,
		MOBILE
	};

	// EMError
	public ref class EMConnectionListener : public EMBase {
	public:
		delegate void D_onConnect();
		delegate void D_onDisconnect(const EMError^ error);
		delegate void D_onPong();
	public:
		D_onConnect^ onConntect = nullptr;
		D_onDisconnect^ onDisconnect = nullptr;
		D_onPong^ onPong = nullptr;
	};

	public ref class EMClient : EMBase
	{
	public:
		~EMClient();

		/**
		* \brief Get the chat client with configs.
		*
		* Note: Caller should delete the client when it is not used any more.
		* @param  chat configurations.
		* @return EMChatClient instance.
		*/
		static EMClient^ create(EMChatConfigs^ configs);

		/**
		* \brief Login with user name and password.
		*
		* Note: Blocking and time consuming operation.
		* @param:  user name and password
		* @return login result, EMError::EM_NO_ERROR means success, others means fail. @see EMError
		*/
		EMError^ login(String^ username, const String^ password);

		/**
		* \brief Login with user name and token.
		*
		* Note: Blocking and time consuming operation.
		* @param:  user name and token
		* @return login result, EMError::EM_NO_ERROR means success, others means fail. @see EMError
		*/
		//EMError^ loginWithToken(String^ username, const String^ token);

		/**
		* \brief Logout current user.
		*
		* @param  NA
		* @return NA.
		*/
		void logout();

		/**
		* \brief Get info of current logoin user.
		*
		* @param  NA
		* @return Login info.
		*/
		//const EMLoginInfo^ getLoginInfo();

		/**
		* \brief register connection listener.
		*
		* @param  EMConnectionListenerPtr
		* @return NA.
		*/
		void addConnectionListener(EMConnectionListener^ listener);

		/**
		* \brief remove connection listener.
		*
		* @param  EMConnectionListenerPtr
		* @return NA.
		*/
		void removeConnectionListener(EMConnectionListener^ listener);

		/**
		* \brief Register a new account with user name and password.
		*
		* Note: Blocking and time consuming operation.
		* @param  user name and password
		* @return register result, EMError::EM_NO_ERROR means success, others means fail.
		*/
		EMError^ createAccount(const String^ username, const String^ password);

		/**
		* \brief get the chat configs.
		*
		* Note: NA.
		* @param NA
		* @return EMChatConfigPtr.
		*/
		EMChatConfigs^ getChatConfigs();

		/**
		* \brief Get chat manager to handle the message operation.
		*
		* @param  NA
		* @return chat manager instance.
		*/
		EMChatManager^ getChatManager();

		/**
		* \brief Get contact manager to manage the contacts.
		*
		* @param  NA
		* @return contact manager instance.
		*/
		EMContactManager^ getContactManager();

		/**
		* \brief Get group manager to manage the group.
		*
		* @param  NA
		* @return group manager instance.
		*/
		EMGroupManager^ getGroupManager();

		/**
		* \brief Get call manager to handle the voice/video call.
		*
		* Note: not release yet, coming soon
		* @param  NA
		* @return call manager instance.
		*/
#if ENABLE_CALL
		EMCallManagerInterface^ getCallManager();
#endif
		/**
		* \brief call this method to notify SDK the network change.
		*
		* @param  EMNetworkType
		* @return NA.
		*/
		virtual void onNetworkChanged(EMNetworkType to);

		void reconnect();
		void disconnect();

		/*
		// method from EMConnectionListener
		void onConnect();
		void onDisconnect(EMError^ error);
		void onPong();
		*/

	private:
		EMClient();
		void* listenerDelegate;
		std::shared_ptr<easemob::EMClient>& getImpl();

		EMChatManager^ mChatManager;
		EMContactManager^ mContactManager;
		EMGroupManager^ mGroupManager;
		EMChatConfigs^ mChatConfigs;
	};
}