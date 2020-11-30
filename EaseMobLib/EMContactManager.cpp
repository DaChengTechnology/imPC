#include "pch.h"
#include "EMContactManager.h"
#include <emcontactlistener.h>
#include <emcontactmanager_interface.h>
#include <vector>
#include <gcroot.h>
#include "EMUtils.h"

using namespace System;
using namespace System::Threading;


namespace EaseMobLib {
	public class EMContactListenerDelegate : easemob::EMContactListener {
	public:
		EMContactListenerDelegate() {
			lockObj = gcnew Object();
		}

		~EMContactListenerDelegate() {
			delete lockObj;
		}

		void addListener(EaseMobLib::EMContactListener^ listener) {
			Monitor::Enter(lockObj);
			bool contains = false;
			for (gcroot<EaseMobLib::EMContactListener^> _listener : listeners) {
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

		void removeListener(EaseMobLib::EMContactListener^ listener) {
			Monitor::Enter(lockObj);
			for (auto iter = listeners.begin(); iter != listeners.end(); iter++) {
				if ((*iter)->Equals(listener)) {
					listeners.erase(iter);
					break;
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void onContactAdded(const std::string& username) {
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMContactListener ^ listener in listeners) {
				if (listener->onContactAdded != nullptr) {
					listener->onContactAdded->Invoke(getCSString(username));
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void onContactDeleted(const std::string& username) {
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMContactListener ^ listener in listeners) {
				if (listener->onContactDeleted != nullptr) {
					listener->onContactDeleted->Invoke(getCSString(username));
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void onContactInvited(const std::string& username, std::string& reason) {
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMContactListener ^ listener in listeners) {
				if (listener->onContactInvited != nullptr) {
					listener->onContactInvited->Invoke(getCSString(username), getCSString(reason));
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void onContactAgreed(const std::string& username) {
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMContactListener ^ listener in listeners) {
				if (listener->onContactAgreed != nullptr) {
					listener->onContactAgreed->Invoke(getCSString(username));
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void onContactRefused(const std::string& username) {
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMContactListener ^ listener in listeners) {
				if (listener->onContactRefused != nullptr) {
					listener->onContactRefused->Invoke(getCSString(username));
				}
			}
			Monitor::Exit(lockObj);
		}
	private:
		gcroot<Object^> lockObj;
		std::vector<gcroot<EaseMobLib::EMContactListener^> > listeners;
	};

	EMContactManager::EMContactManager(easemob::EMContactManagerInterface* p) {
		mImpl = p;
		listenerDelegate = new EMContactListenerDelegate();
		getImpl()->registerContactListener((easemob::EMContactListener*)listenerDelegate);
	}

	EMContactManager::~EMContactManager() {
		getImpl()->removeContactListener((easemob::EMContactListener*)listenerDelegate);
		delete (easemob::EMContactListener*)listenerDelegate;
		listenerDelegate = nullptr;
	}

	/**
	* \brief register contact status change listener
	*
	* @param  listener contact status change listener
	*/
	void EMContactManager::registerContactListener(EMContactListener^ listener) {
		((EMContactListenerDelegate*)listenerDelegate)->addListener(listener);
	}

	/**
	* \brief remove registration of contact status change listener
	*
	* @param  listener contact status change listener
	*/
	void EMContactManager::removeContactListener(EMContactListener^ listener) {
		((EMContactListenerDelegate*)listenerDelegate)->removeListener(listener);
	}

	/**
	* \brief retrieve current user's friend list from server.
	*
	* @return  contact list
	*/
	/*cli::array<const String^>^ EMContactManager::allContacts(EMError^ error) {
		return getContactsFromServer(error);
	}*/

	/**
	* \brief retrieve current user's friend list from server.
	*
	* @return  contact list
	*/
	//cli::array<const String^>^ EMContactManager::getContactsFromServer(EMError^ csError) {
	//	easemob::EMErrorPtr% ptr = csError->getNative<easemob::EMErrorPtr>();
	//	easemob::EMError error;
	//	std::vector<std::string> contacts = getImpl()->getContactsFromServer(error);
	//	ptr.reset(new easemob::EMError(error));
	//	return fillCSArray(contacts);
	//}

	///**
	//* \brief retrieve current user's friend list from local database.
	//*
	//* @return  contact list
	//*/
	//cli::array<const String^>^ EMContactManager::getContactsFromDB(EMError^ csError) {
	//	easemob::EMErrorPtr% ptr = csError->getNative<easemob::EMErrorPtr>();
	//	easemob::EMError error;
	//	std::vector<std::string> contacts = getImpl()->getContactsFromServer(error);
	//	ptr.reset(new easemob::EMError(error));
	//	return fillCSArray(contacts);
	//}

	/**
	* \brief invite contact to be friend, need contact accept.
	*
	* @param  username contact to be invited.
	* @param  message contact will receive the message when got invitation.
	*/
	void EMContactManager::inviteContact(const String^ username, const String^ message, EMError^ csError) {
		easemob::EMErrorPtr% ptr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMError error;
		getImpl()->inviteContact(extractCSString(username), extractCSString(message), error);
		ptr.reset(new easemob::EMError(error));
	}

	/**
	* \brief delete contact from contact list.
	* contact part will auto be removed friend relationship.
	*
	* @param  username contact to be invited.
	*/
	void EMContactManager::deleteContact(const String^ username, EMError^ csError, bool keepConversation) {
		easemob::EMErrorPtr% ptr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMError error;
		getImpl()->deleteContact(extractCSString(username), error);
		ptr.reset(new easemob::EMError(error));
	}

	/**
	* \brief accept contact's invitation
	*
	* @param  username contact who initiate invitation.
	*/
	void EMContactManager::acceptInvitation(const String^ username, EMError^ csError) {
		easemob::EMErrorPtr% ptr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMError error;
		getImpl()->acceptInvitation(extractCSString(username), error);
		ptr.reset(new easemob::EMError(error));
	}

	/**
	* \brief decline contact's invitation
	*
	* @param  username contact who initiate invitation.
	*/
	void EMContactManager::declineInvitation(const String^ username, EMError^ csError) {
		easemob::EMErrorPtr% ptr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMError error;
		getImpl()->declineInvitation(extractCSString(username), error);
		ptr.reset(new easemob::EMError(error));
	}

	// -------------------------- blacklist --------------------------
	/**
	* \brief retrieve black list from memory
	*
	* @return  black list of current user, contacts in the block list can not send message or
	* inviation to user
	*/
	/*cli::array<const String^>^ EMContactManager::blacklist(EMError^ csError) {
		easemob::EMErrorPtr% ptr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMError error;
		std::vector<std::string> blackList = getImpl()->blacklist(error);
		ptr.reset(new easemob::EMError(error));
		return fillCSArray(blackList);
	}*/

	///**
	//* \brief retrieve black list from server
	//*
	//* Note: sync operation.
	//* returned result will also be updated in database.
	//* @return  black list of current user, contacts in the block list can not send message or
	//* inviation to user
	//*/
	//cli::array<const String^>^ EMContactManager::getBlackListFromServer(EMError^ csError) {
	//	easemob::EMErrorPtr% ptr = csError->getNative<easemob::EMErrorPtr>();
	//	easemob::EMError error;
	//	std::vector<std::string> blackList = getImpl()->getBlackListFromServer(error);
	//	ptr.reset(new easemob::EMError(error));
	//	return fillCSArray(blackList);
	//}

	///**
	//* \brief retrieve black list from local database
	//*
	//* @return  black list of current user which stored in local database.
	//*/
	//cli::array<const String^>^ EMContactManager::getBlackListFromDB(EMError^ csError) {
	//	easemob::EMErrorPtr% ptr = csError->getNative<easemob::EMErrorPtr>();
	//	easemob::EMError error;
	//	std::vector<std::string> blackList = getImpl()->getBlackListFromDB(error);
	//	ptr.reset(new easemob::EMError(error));
	//	return fillCSArray(blackList);
	//}

	/**
	* \brief save black list.
	*
	* Note: sync operation.
	* black list will be sent to server and local database will also updated.
	* @param  blacklist contacts in the block list can not send message
	* inviation to user
	*/
	void EMContactManager::saveBlackList(cli::array<String^>^ blacklist, EMError^ csError) {
		easemob::EMErrorPtr% ptr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMError error;
		std::vector<std::string> list;
		extractArray(blacklist, list);
		getImpl()->saveBlackList(list, error);
		ptr.reset(new easemob::EMError(error));
	}

	/**
	* \brief add contact to blacklist
	*
	* Note: sync operation
	* new item will updated to remote server, and also update local database.
	* @param  username contact to be added to blacklist
	* @param  both whether both side will be blocked, if true user also can not subscribe contact's presense. both = false is not work yet, current behaviour is both side conmunication will be blocded.
	*/
	void EMContactManager::addToBlackList(const String^ username, bool both, EMError^ csError) {
		easemob::EMErrorPtr% ptr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMError error;
		getImpl()->addToBlackList(extractCSString(username), both, error);
		ptr.reset(new easemob::EMError(error));
	}

	/**
	* \brief remove contact from black list
	*
	* Note: sync operation
	* new item will updated to remote server, and also update local database.
	* @param  username contact to be removed from blacklist
	*/
	void EMContactManager::removeFromBlackList(const String^ username, EMError^ csError) {
		easemob::EMErrorPtr% ptr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMError error;
		getImpl()->removeFromBlackList(extractCSString(username), error);
		ptr.reset(new easemob::EMError(error));
	}

	easemob::EMContactManagerInterface* EMContactManager::getImpl() {
		return mImpl;
	}
}