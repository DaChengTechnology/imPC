#include "EMChatManager.h"
#include <vector>

#include <gcroot.h>
#include "EMUtils.h"

using namespace System;
using namespace System::Threading;

namespace EaseMobLib {
	public class EMChatManagerListenerDelegate : easemob::EMChatManagerListener {
	public:
		EMChatManagerListenerDelegate() {
			lockObj = gcnew Object();
		}

		~EMChatManagerListenerDelegate() {
		}

		void addListener(EaseMobLib::EMChatManagerListener^ listener) {
			Monitor::Enter(lockObj);
			bool contains = false;
			for (gcroot<EaseMobLib::EMChatManagerListener^> _listener : listeners) {
				if (_listener->Equals(listener)) {
					contains = true;
					break;
				}
			}
			if (!contains) {
				listeners.push_back(listener);
			}
			Monitor::Exit(lockObj);
		}

		void removeListener(EaseMobLib::EMChatManagerListener^ listener) {
			Monitor::Enter(lockObj);
			for (auto iter = listeners.begin(); iter != listeners.end(); iter++) {
				if ((*iter)->Equals(listener)) {
					listeners.erase(iter);
					break;
				}
			}
			Monitor::Exit(lockObj);
		}

		void clearListeners() {
			Monitor::Enter(lockObj);
			listeners.clear();
			Monitor::Exit(lockObj);
		}

		void onReceiveMessages(const easemob::EMMessageList& messages) {
			gcroot<array<EaseMobLib::EMMessage^>^> csMessages = fillCSMessageList(messages);
			Monitor::Enter(lockObj);
			for (gcroot<EaseMobLib::EMChatManagerListener^> listener : listeners) {
				if (listener->onReceiveMessages != nullptr) {
					listener->onReceiveMessages->Invoke(csMessages);
				}
			}
			Monitor::Exit(lockObj);
		}

		void onReceiveCmdMessages(const easemob::EMMessageList& messages) {
			gcroot<array<EaseMobLib::EMMessage^>^> csMessages = fillCSMessageList(messages);
			Monitor::Enter(lockObj);
			for (gcroot<EaseMobLib::EMChatManagerListener^> listener : listeners) {
				if (listener->onReceiveCmdMessages != nullptr) {
					listener->onReceiveCmdMessages->Invoke(csMessages);
				}
			}
			Monitor::Exit(lockObj);

		}

		void onMessageStatusChanged(const easemob::EMMessagePtr message, const easemob::EMErrorPtr error) {
			Monitor::Enter(lockObj);
			for (gcroot<EaseMobLib::EMChatManagerListener^> listener : listeners) {
				if (listener->onMessageStatusChanged != nullptr) {
					listener->onMessageStatusChanged->Invoke(getCSMessage(message), getCSError(error));
				}
			}
			Monitor::Exit(lockObj);
		}

		void onMessageAttachmentsStatusChanged(const easemob::EMMessagePtr message, const easemob::EMErrorPtr error) {
			Monitor::Enter(lockObj);
			for (gcroot<EaseMobLib::EMChatManagerListener^> listener : listeners) {
				if (listener->onMessageAttachmentsStatusChanged != nullptr) {
					listener->onMessageAttachmentsStatusChanged->Invoke(getCSMessage(message), getCSError(error));
				}
			}
			Monitor::Exit(lockObj);
		}

		void onReceiveHasReadAcks(const easemob::EMMessageList& messages) {
			gcroot<array<EaseMobLib::EMMessage^>^> csMessages = fillCSMessageList(messages);
			Monitor::Enter(lockObj);
			for (gcroot<EaseMobLib::EMChatManagerListener^> listener : listeners) {
				if (listener->onReceiveHasReadAcks != nullptr) {
					listener->onReceiveHasReadAcks->Invoke(csMessages);
				}
			}
			Monitor::Exit(lockObj);
		}

		void onReceiveHasDeliveredAcks(const easemob::EMMessageList& messages) {
			gcroot<array<EaseMobLib::EMMessage^>^> csMessages = fillCSMessageList(messages);
			Monitor::Enter(lockObj);
			for (gcroot<EaseMobLib::EMChatManagerListener^> listener : listeners) {
				if (listener->onReceiveHasDeliveredAcks != nullptr) {
					listener->onReceiveHasDeliveredAcks->Invoke(csMessages);
				}
			}
			Monitor::Exit(lockObj);
		}

		void onUpdateConversationList(const easemob::EMConversationList& conversations) {
			gcroot<array<EaseMobLib::EMConversation^>^> csConversationList = fillCSConversationList(conversations);
			Monitor::Enter(lockObj);
			for each (gcroot<EaseMobLib::EMChatManagerListener^> listener in listeners) {
				if (listener->onUpdateConversationList != nullptr) {
					listener->onUpdateConversationList->Invoke(csConversationList);
				}
			}
			Monitor::Exit(lockObj);
		}

	private:
		gcroot<Object^> lockObj;
		std::vector<gcroot<EaseMobLib::EMChatManagerListener^> > listeners;
	};


	EMChatManager::EMChatManager(easemob::EMChatManagerInterface* p) {
		mImpl = p;
		listenerDelegate = new EMChatManagerListenerDelegate();
		getImpl()->addListener((easemob::EMChatManagerListener*)listenerDelegate);
	}

	EMChatManager::~EMChatManager() {
		getImpl()->removeListener((easemob::EMChatManagerListener*)listenerDelegate);
		delete listenerDelegate;
		listenerDelegate = nullptr;
	}

	/**
	* \brief Send a message.
	*
	* Note: Will callback user by EMChatManagerListener if user doesn't provide a callback in the message or callback return false.
	* @param  The message to send.
	* @return NA
	*/
	void EMChatManager::sendMessage(EMMessage^ csMsg) {
		easemob::EMMessagePtr& msg = csMsg->getNative<easemob::EMMessagePtr>();
		getImpl()->sendMessage(msg);
	}

	/**
	* \brief Send read ask for a message.
	*
	* @param  The message to send read ack.
	* @return NA
	*/
	void EMChatManager::sendReadAckForMessage(EMMessage^ csMsg) {
		easemob::EMMessagePtr& msg = csMsg->getNative<easemob::EMMessagePtr>();
		getImpl()->sendMessage(msg);
	}

	/**
	* \brief Resend a message.
	*
	* Note: Will callback user by EMChatManagerListener if user doesn't provide a callback in the message or callback return false.
	* @param  The message to resend.
	* @return NA
	*/
	void EMChatManager::resendMessage(EMMessage^ csMsg) {
		easemob::EMMessagePtr& msg = csMsg->getNative<easemob::EMMessagePtr>();
		getImpl()->resendMessage(msg);
	}

	/**
	* \brief Download thumbnail for image or video message.
	*
	* Note: Image or video message thumbnail is downloaded automatically,
	so user should NOT call this method except automatic download failed.
	And too, SDK will callback the user by EMChatManagerListener if user doesn't provide a callback in the message or callback return false.
	* @param  The message to download thumbnail.
	* @return NA
	*/
	void EMChatManager::downloadMessageThumbnail(EMMessage^ csMsg) {
		easemob::EMMessagePtr& msg = csMsg->getNative<easemob::EMMessagePtr>();
		getImpl()->downloadMessageThumbnail(msg);
	}

	/**
	* \brief Download attachment of a message.
	*
	* Note: User should call this method to download file, voice, image, video.
	And too, SDK will callback the user by EMChatManagerListener if user doesn't provide a callback or callback return false.
	* @param  The message to download attachment.
	* @return NA
	*/
	void EMChatManager::downloadMessageAttachments(EMMessage^ csMsg) {
		easemob::EMMessagePtr& msg = csMsg->getNative<easemob::EMMessagePtr>();
		getImpl()->downloadMessageAttachments(msg);
	}

	/**
	* \brief Remove a conversation from DB and the memory.
	*
	* Note: Before remove a conversation, all conversations must have loaded from DB.
	* @param  The conversation id.
	* @param  The flag of whether remove the messages belongs to this conversation.
	* @return NA
	*/
	void EMChatManager::removeConversation(String^ conversationId, bool isRemoveMessages) {
		getImpl()->removeConversation(extractCSString(conversationId), isRemoveMessages);
	}

	/**
	* \brief Get a conversation.
	*
	* Note: All conversations must have loaded from DB.
	* @param  The conversation id.
	* @param  The conversation type.
	* @param  The flag of whether created a conversation if it isn't exist.
	* @return The conversation
	*/
	EMConversation^ EMChatManager::conversationWithType(String^ conversationId, EMConversationType type, bool createIfNotExist) {
		easemob::EMConversationPtr conv = getImpl()->conversationWithType(extractCSString(conversationId), (easemob::EMConversation::EMConversationType)type, createIfNotExist);
		if (conv == nullptr) {
			return nullptr;
		}
		easemob::EMConversationPtr* ptr = new easemob::EMConversationPtr(conv);
		return getCSConversation(ptr);
	}

	/**
	* \brief Get all conversations from memory.
	*
	* Note: All conversations must have loaded from DB.
	* @param  NA
	* @return The conversation list
	*/
	EMConversationList^ EMChatManager::getConversations() {
		std::vector<easemob::EMConversationPtr> conversationList = getImpl()->getConversations();;
		array<EMConversation^>^ csConversationList = gcnew array<EMConversation^>(conversationList.size());
		int i = 0;
		for (easemob::EMConversationPtr conversation : conversationList) {
			easemob::EMConversationPtr* ptr = new easemob::EMConversationPtr(conversation);
			csConversationList[i] = getCSConversation(ptr);
			i++;
		}
		return csConversationList;
	}

	/**
	* \brief Get all conversations from DB.
	*
	* @param  NA
	* @return The conversation list
	*/
	EMConversationList^ EMChatManager::loadAllConversationsFromDB() {
		std::vector<easemob::EMConversationPtr> conversationList = getImpl()->loadAllConversationsFromDB();;
		array<EMConversation^>^ csConversationList = gcnew array<EMConversation^>(conversationList.size());
		int i = 0;
		for (easemob::EMConversationPtr conversation : conversationList) {
			easemob::EMConversationPtr* ptr = new easemob::EMConversationPtr(conversation);
			csConversationList[i] = getCSConversation(ptr);
			i++;
		}
		return csConversationList;
	}

	/**
	* \brief Add a listener to chat manager.
	*
	* @param  NA
	* @return NA
	*/
	void EMChatManager::addListener(EMChatManagerListener^ listener) {
		((EMChatManagerListenerDelegate*)listenerDelegate)->addListener(listener);
	}

	/**
	* \brief Remove a listener.
	*
	* @param  NA
	* @return NA
	*/
	void EMChatManager::removeListener(EMChatManagerListener^ listener) {
		((EMChatManagerListenerDelegate*)listenerDelegate)->removeListener(listener);
	}

	/**
	* \brief Remove all the listeners.
	*
	* @param  NA
	* @return NA
	*/
	void EMChatManager::clearListeners() {
		((EMChatManagerListenerDelegate*)listenerDelegate)->clearListeners();
	}

	/**
	* \brief Application can customize encrypt method through EMEncryptProvider
	*
	* Note: If EMConfigManager#KEY_USE_ENCRYPTION is true, but don't provider encryptprovider,
	* SDK will use default encrypt method.
	* @param  EMEncryptProvider Customized encrypt method provider.
	* @return NA
	*/
	//void setEncryptProvider(EMEncryptProviderInterface *provider);

	/**
	* \brief Get encrypt method being used.
	*
	* @param createIfNotExist If true, SDK will create a default encryptProvider, when there is no encryptProvider exists.
	* @return Encrypt method being used.
	*/
	//EMEncryptProviderInterface *getEncryptProvider(bool createIfNotExist = false);

	/**
	* \brief Insert messages.
	* @param  The messages to insert.
	* @return NA
	*/
	bool EMChatManager::insertMessages(EMMessageList^ csList) {
		easemob::EMMessageList list;
		for each (EMMessage ^ csMsg in csList) {
			easemob::EMMessagePtr& msg = csMsg->getNative<easemob::EMMessagePtr>();
			list.push_back(msg);
		}
		return getImpl()->insertMessages(list);
	}

	/**
	* \brief Get message by message Id.
	*
	* @param messageId
	* @return EMMessagePtr
	*/
	EMMessage^ EMChatManager::getMessage(String^ messageId) {
		easemob::EMMessagePtr msg = getImpl()->getMessage(extractCSString(messageId));
		easemob::EMMessagePtr* ptr = new easemob::EMMessagePtr(msg);
		return getCSMessage(ptr);
	}

	/**
	* \brief Upload log to server.
	*/
	void EMChatManager::uploadLog() {
		getImpl()->uploadLog();
	}

	easemob::EMChatManagerInterface* EMChatManager::getImpl() {
		return mImpl;
	}
}