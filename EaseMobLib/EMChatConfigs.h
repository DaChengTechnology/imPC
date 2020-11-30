#pragma once
#include <emchatconfigs.h>
#include "emsbsase.h"

namespace EaseMobLib {

	public enum class OSType
	{
		OS_IOS = 0,
		OS_ANDROID = 1,
		OS_LINUX = 2,
		OS_OSX = 3,
		OS_WIN = 4,
		OS_OTHER = 16,
	};

	public enum class EMLogLevel
	{
		DEBUG_LEVEL,
		WARNING_LEVEL,
		ERROR_LEVEL
	};
	public ref class EMChatConfigs:EMBase
	{
	public:

		//EMChatConfigs(const String^ resourcePath, const String^ workPath, const String^ appkey);

		EMChatConfigs(const String^ resourcePath, const String^ workPath, const String^ appkey, unsigned int deviceId);

		~EMChatConfigs();

		void setUsingHttps(bool usingHttps);

		bool getUsingHttps();

		void setUseEncryption(bool useEncryption);

		bool getUseEncryption();

		void setSortMessageByServerTime(bool sortByServerTime);

		bool getSortMessageByServerTime();

		// disable NetCallback
		/*
		void setNetCallback(const EMNetCallback^ callback);

		EMNetCallbackPtr getNetCallback() {
			return mNetCallback;
		}
		*/

		/**
		* \brief Get the resource path.
		*
		* @param  NA.
		* @return resource path.
		*/
		const String^ getResourcePath();

		/**
		* \brief Get the work path.
		*
		* @param  NA.
		* @return work path.
		*/
		const String^ getWorkPath();

		/**
		* \brief Set the log path.
		*
		* Note: This path can't change in run time.
		* @param  NA.
		* @return log path.
		*/
		void setLogPath(const String^ path);

		const String^ getLogPath();

		/**
		* \brief Set the download path.
		*
		* Note: This path can't change in run time.
		* @param  NA.
		* @return download path.
		*/
		void setDownloadPath(const String^ path);

		const String^ getDownloadPath();

		/**
		* \brief Get the app key.
		*
		* @param  NA.
		* @return app key.
		*/
		void setAppKey(const String^ appKey);

		const String^ getAppKey();

		/**
		* \brief set sandbox mode.
		*
		* Default is false.
		* @param  true or false.
		* @return NA.
		*/
		void setIsSandboxMode(bool b);

		/**
		* \brief get sandbox mode.
		*
		* @param  NA.
		* @return true or false.
		*/
		bool getIsSandboxMode();

		/**
		* \brief set if output the log to console.
		*
		* Default is false.
		* @param  true or false.
		* @return NA.
		*/
		void setEnableConsoleLog(bool b);

		/**
		* \brief get if output the log to console.
		*
		* @param  true or false.
		* @return NA.
		*/
		bool getEnableConsoleLog();

		/**
		* \brief set if auto accept friend invitation.
		*
		* Default is false.
		* @param  true or false.
		* @return NA.
		*/
		void setAutoAcceptFriend(bool b);

		/**
		* \brief get if auto accept friend invitation.
		*
		* @param  NA.
		* @return true or false.
		*/
		bool getAutoAcceptFriend();

		/**
		* \brief set if auto accept group invitation.
		*
		* Default is true.
		* @param  true or false.
		* @return NA.
		*/
		void setAutoAcceptGroup(bool b);

		/**
		* \brief get if auto accept group invitation.
		*
		* @param  NA.
		* @return true or false.
		*/
		bool getAutoAcceptGroup();

		/**
		* \brief set if need message read ack.
		*
		* Default is true.
		* @param  true or false.
		* @return NA.
		*/
		void setRequireReadAck(bool b);

		/**
		* \brief get if need message read ack.
		*
		* @param  NA.
		* @return true or false.
		*/
		bool getRequireReadAck();

		/**
		* \brief set if need message delivery ack.
		*
		* Default is false.
		* @param  true or false.
		* @return NA.
		*/
		void setRequireDeliveryAck(bool b);

		/**
		* \brief get if need message delivery ack.
		*
		* @param  NA.
		* @return true or false.
		*/
		bool getRequireDeliveryAck();

		/**
		* \brief set if need load all conversation when login.
		*
		* Default is true.
		* @param  true or false.
		* @return NA.
		*/
		void setAutoConversationLoaded(bool b);

		/**
		* \brief get if load all conversation when login.
		*
		* @param  NA.
		* @return true or false.
		*/
		bool getAutoConversationLoaded();

		/**
		* \brief set if delete message when exit group.
		*
		* Default is true.
		* @param  true or false.
		* @return NA.
		*/
		void setDeleteMessageAsExitGroup(bool b);

		/**
		* \brief get if delete message when exit group.
		*
		* @param  NA.
		* @return true or false.
		*/
		bool getDeleteMessageAsExitGroup();

		/**
		* \brief set if chatroom owner can leave.
		*
		* Default is true.
		* @param  true or false.
		* @return NA.
		*/
		void setIsChatroomOwnerLeaveAllowed(bool b);

		/**
		* \brief get if chatroom owner can leave.
		*
		* @param  NA.
		* @return true or false.
		*/
		bool getIsChatroomOwnerLeaveAllowed();

		/**
		* \brief set the number of message load at first time.
		*
		* Default is 20.
		* @param  true or false.
		* @return NA.
		*/
		void setNumOfMessageLoaded(int n);

		/**
		* \brief get the number of message load at first time.
		*
		* Default is 20.
		* @param
		* @return number of messages.
		*/
		int  getNumOfMessageLoaded();

		/**
		* \brief set os type.
		*
		* @param  os type.
		* @return NA.
		*/
		void setOs(const OSType os);

		/**
		* \brief get os type.
		*
		* @param  NA.
		* @return os type.
		*/
		OSType getOs();

		/**
		* \brief set os version.
		*
		* @param  os version.
		* @return NA.
		*/
		void setOsVersion(const String^ version);

		/**
		* \brief get os version.
		*
		* @param  NA.
		* @return os version.
		*/
		const String^ getOsVersion();

		/**
		* \brief set sdk version.
		*
		* @param  sdk version.
		* @return NA.
		*/
		void setSdkVersion(const String^ version);

		/**
		* \brief get sdk version.
		*
		* @param  NA.
		* @return sdk version.
		*/
		const String^ getSdkVersion();

		/**
		* \brief get device unique id.
		*
		* @param  NA.
		* @return device unique id.
		*/
		unsigned int getDeviceID();

		/**
		* \brief set client resource
		*
		* @param resource
		*/
		void setClientResource(const String^ resource);

		/**
		* \brief get client resource
		*
		* @return resource
		*/
		const String^ clientResource();

		/**
		* \brief Set log output level
		*
		* @param  log output level
		*/
		void setLogLevel(EMLogLevel level);

	private:
		easemob::EMChatConfigsPtr& getImpl();
	};
}