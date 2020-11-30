#include "EMClient.h"
#include <gcroot.h>

using namespace System;
using namespace System::Text;
using namespace System::Threading;

#pragma managed

namespace easemob {
	typedef std::shared_ptr<easemob::EMClient> EMClientPtr;
}

namespace EaseMobLib {
	class EMConnectionListenerDelegate : easemob::EMConnectionListener {
	public:
		EMConnectionListenerDelegate() {
			lockObj = gcnew Object();
		}
		virtual ~EMConnectionListenerDelegate() {
			delete lockObj;
		}

		void addListener(EaseMobLib::EMConnectionListener^ listener) {
			Monitor::Enter(lockObj);
			bool contains = false;
			for each (EaseMobLib::EMConnectionListener ^ _listener in listeners) {
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

		void removeListener(EaseMobLib::EMConnectionListener^ listener) {
			Monitor::Enter(lockObj);
			for (auto iter = listeners.begin(); iter != listeners.end(); iter++) {
				if ((*iter)->Equals(listener)) {
					listeners.erase(iter);
					break;
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void onConnect() {
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMConnectionListener ^ listener in listeners) {
				if (listener->onConntect != nullptr) {
					listener->onConntect->Invoke();
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void onDisconnect(easemob::EMErrorPtr error) {
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMConnectionListener ^ listener in listeners) {
				if (listener->onDisconnect != nullptr) {
					listener->onDisconnect->Invoke(getCSError(error));
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void onPong() {
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMConnectionListener ^ listener in listeners) {
				if (listener->onPong != nullptr) {
					listener->onPong->Invoke();
				}
			}
			Monitor::Exit(lockObj);
		}

	private:
		gcroot<Object^> lockObj;
		std::vector<gcroot<EaseMobLib::EMConnectionListener^> > listeners;
	};

	EMClient::EMClient() {
	}

	EMClient::~EMClient() {
		getImpl()->removeConnectionListener((easemob::EMConnectionListener*)listenerDelegate);
		delete (EMConnectionListenerDelegate*)listenerDelegate;
		nativeFinalize<easemob::EMClientPtr>();
	}

	/**
	* \brief Get the chat client with configs.
	*
	* Note: Caller should delete the client when it is not used any more.
	* @param  chat configurations.
	* @return EMChatClient instance.
	*/
	EMClient^ EMClient::create(EMChatConfigs^ configs) {
		easemob::EMChatConfigsPtr& ptrConfig = configs->getNative<easemob::EMChatConfigsPtr>();
		easemob::EMClient* client = easemob::EMClient::create(ptrConfig);
		std::shared_ptr<easemob::EMClient>* ptrClient = new std::shared_ptr<easemob::EMClient>(client);
		EMClient^ csClient = gcnew EMClient;
		csClient->setNativeHandler<std::shared_ptr<easemob::EMClient> >((void*)ptrClient);
		csClient->mChatManager = gcnew EMChatManager(&client->getChatManager());
		csClient->mContactManager = gcnew EMContactManager(&client->getContactManager());
		csClient->mGroupManager = gcnew EMGroupManager(&client->getGroupManager());
		csClient->mChatConfigs = configs;
		csClient->listenerDelegate = new EMConnectionListenerDelegate();

		(*ptrClient)->addConnectionListener((easemob::EMConnectionListener*)csClient->listenerDelegate);

		return csClient;
	}

	/**
	* \brief Login with user name and password, username will auto change to lower case.
	*
	* Note: Blocking and time consuming operation.
	* @param:  user name and password
	* @return login result, EMError::EM_NO_ERROR means success, others means fail. @see EMError
	*/
	EMError^ EMClient::login(String^ username, const String^ password) {
		EMError^ csError = gcnew EMError();
		easemob::EMClientPtr& client = this->getNative<easemob::EMClientPtr>();
		if (username == nullptr) {
			int v = (int)easemob::EMError::INVALID_USER_NAME;;
			csError->errorCode = (EMErrorCode)v;
			csError->description = "Invalid username";
			return csError;
		}
		if (password == nullptr) {
			int v = (int)easemob::EMError::INVALID_PASSWORD;
			csError->errorCode = (EMErrorCode)v;
			csError->description = "Invalid password";
			return csError;
		}
		// to lower
		std::string _username = extractCSString(username->ToLower());
		std::string _password = extractCSString(password);
		easemob::EMErrorPtr error = client->login(_username, _password);
		csError->setNativeHandler<easemob::EMErrorPtr>(&error);
		return csError;
	}

	/**
	* \brief Logout current user.
	*
	* @param  NA
	* @return NA.
	*/
	void EMClient::logout() {
		easemob::EMClientPtr& client = this->getNative<easemob::EMClientPtr>();
		if (client == nullptr) {
			return;
		}
		client->logout();
	}

	/**
	* \brief Get info of current logoin user.
	*
	* @param  NA
	* @return Login info.
	*/
	/*const EMLoginInfo^ EMClient::getLoginInfo() {
		easemob::EMClientPtr& client = this->getNative<easemob::EMClientPtr>();
		if (client == nullptr) {
			return nullptr;
		}
		const easemob::EMLoginInfo& info = client->getLoginInfo();
		EaseMobLib::EMLoginInfo^ csInfo = gcnew EaseMobLib::EMLoginInfo(info.loginUser(), info.loginPassword(), info.loginToken());
		return csInfo;
	}*/

	/**
	* \brief register connection listener.
	*
	* @param  EMConnectionListenerPtr
	* @return NA.
	*/
	void EMClient::addConnectionListener(EMConnectionListener^ listener) {
		((EMConnectionListenerDelegate*)this->listenerDelegate)->addListener(listener);
	}

	/**
	* \brief remove connection listener.
	*
	* @param  EMConnectionListenerPtr
	* @return NA.
	*/
	void EMClient::removeConnectionListener(EMConnectionListener^ listener) {
		((EMConnectionListenerDelegate*)this->listenerDelegate)->removeListener(listener);
	}

	/**
	* \brief Register a new account with user name and password.
	*
	* Note: Blocking and time consuming operation.
	* @param  user name and password
	* @return register result, EMError::EM_NO_ERROR means success, others means fail.
	*/
	EMError^ EMClient::createAccount(const String^ username, const String^ password) {
		EMError^ csError = gcnew EMError();
		easemob::EMClientPtr& client = this->getNative<easemob::EMClientPtr>();
		if (username == nullptr) {
			int v = (int)easemob::EMError::INVALID_USER_NAME;;
			csError->errorCode = (EMErrorCode)v;
			csError->description = "Invalid username";
			return csError;
		}
		if (password == nullptr) {
			int v = (int)easemob::EMError::INVALID_PASSWORD;
			csError->errorCode = (EMErrorCode)v;
			csError->description = "Invalid password";
			return csError;
		}
		// to lower
		std::string _username = extractCSString(((String^)username)->ToLower());
		std::string _password = extractCSString(password);
		easemob::EMErrorPtr error = client->createAccount(_username, _password);
		csError->setNativeHandler<easemob::EMErrorPtr>(&error);
		return csError;
	}

	/**
	* \brief get the chat configs.
	*
	* Note: NA.
	* @param NA
	* @return EMChatConfigPtr.
	*/
	EMChatConfigs^ EMClient::getChatConfigs() { return mChatConfigs; }

	/**
	* \brief Get chat manager to handle the message operation.
	*
	* @param  NA
	* @return chat manager instance.
	*/
	EMChatManager^ EMClient::getChatManager() { return mChatManager; }

	/**
	* \brief Get contact manager to manage the contacts.
	*
	* @param  NA
	* @return contact manager instance.
	*/
	EMContactManager^ EMClient::getContactManager() { return mContactManager; }

	/**
	* \brief Get group manager to manage the group.
	*
	* @param  NA
	* @return group manager instance.
	*/
	EMGroupManager^ EMClient::getGroupManager() { return mGroupManager; }

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
	void EMClient::onNetworkChanged(EaseMobLib::EMNetworkType to) {
		getImpl()->onNetworkChanged((easemob::EMNetworkListener::EMNetworkType)to);
	}

	void EMClient::reconnect() {
		getImpl()->reconnect();
	}

	void EMClient::disconnect() {
		getImpl()->disconnect();
	}

	std::shared_ptr<easemob::EMClient>& EMClient::getImpl() {
		return getNative<std::shared_ptr<easemob::EMClient>>();
	}
}