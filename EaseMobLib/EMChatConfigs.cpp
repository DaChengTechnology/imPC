#include "EMChatConfigs.h"
#include "EMUtils.h"

namespace EaseMobLib {

	EMChatConfigs::EMChatConfigs(const String^ resourcePath, const String^ workPath, const String^ appkey, unsigned int deviceId) {
		easemob::EMChatConfigsPtr ptr(new easemob::EMChatConfigs(
			extractCSString(resourcePath),
			extractCSString(workPath),
			extractCSString(appkey),
			deviceId
		));
		setNativeHandler<easemob::EMChatConfigsPtr>(&ptr);
	}

	EMChatConfigs::~EMChatConfigs() {
		nativeFinalize<easemob::EMChatConfigsPtr>();
	}

	void EMChatConfigs::setUsingHttps(bool usingHttps) {
		getImpl()->setUsingHttps(usingHttps);
	}

	bool EMChatConfigs::getUsingHttps() {
		return getImpl()->getUsingHttps();
	}

	void EMChatConfigs::setUseEncryption(bool useEncryption) {
		getImpl()->setUseEncryption(useEncryption);
	}

	bool EMChatConfigs::getUseEncryption() {
		return getImpl()->getUseEncryption();
	}

	void EMChatConfigs::setSortMessageByServerTime(bool sortByServerTime) {
		getImpl()->setSortMessageByServerTime(sortByServerTime);
	}

	bool EMChatConfigs::getSortMessageByServerTime() {
		return getImpl()->getSortMessageByServerTime();
	}

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
	const String^ EMChatConfigs::getResourcePath() {
		return getCSString(getImpl()->getResourcePath());
	}

	/**
	* \brief Get the work path.
	*
	* @param  NA.
	* @return work path.
	*/
	const String^ EMChatConfigs::getWorkPath() {
		return getCSString(getImpl()->getWorkPath());
	}

	/**
	* \brief Set the log path.
	*
	* Note: This path can't change in run time.
	* @param  NA.
	* @return log path.
	*/
	void EMChatConfigs::setLogPath(const String^ path) {
		getImpl()->setLogPath(extractCSString(path));
	}

	const String^ EMChatConfigs::getLogPath() {
		return getCSString(getImpl()->getLogPath());
	}

	/**
	* \brief Set the download path.
	*
	* Note: This path can't change in run time.
	* @param  NA.
	* @return download path.
	*/
	void EMChatConfigs::setDownloadPath(const String^ path) {
		getImpl()->setDownloadPath(extractCSString(path));
	}

	const String^ EMChatConfigs::getDownloadPath() {
		return getCSString(getImpl()->getDownloadPath());
	}

	/**
	* \brief Get the app key.
	*
	* @param  NA.
	* @return app key.
	*/
	void EMChatConfigs::setAppKey(const String^ appKey) {
		getImpl()->setAppKey(extractCSString(appKey));
	}

	const String^ EMChatConfigs::getAppKey() {
		return getCSString(getImpl()->getAppKey());
	}

	/**
	* \brief set sandbox mode.
	*
	* Default is false.
	* @param  true or false.
	* @return NA.
	*/
	void EMChatConfigs::setIsSandboxMode(bool b) {
		getImpl()->setIsSandboxMode(b);
	}

	/**
	* \brief get sandbox mode.
	*
	* @param  NA.
	* @return true or false.
	*/
	bool EMChatConfigs::getIsSandboxMode() {
		return getImpl()->getIsSandboxMode();
	}

	/**
	* \brief set if output the log to console.
	*
	* Default is false.
	* @param  true or false.
	* @return NA.
	*/
	void EMChatConfigs::setEnableConsoleLog(bool b) {
		getImpl()->setEnableConsoleLog(b);
	}

	/**
	* \brief get if output the log to console.
	*
	* @param  true or false.
	* @return NA.
	*/
	bool EMChatConfigs::getEnableConsoleLog() {
		return getImpl()->getEnableConsoleLog();
	}

	/**
	* \brief get if auto accept friend invitation.
	*
	* @param  NA.
	* @return true or false.
	*/
	bool EMChatConfigs::getAutoAcceptFriend() {
		return getImpl()->getAutoAcceptFriend();
	}

	/**
	* \brief set if auto accept friend invitation.
	*
	* Default is false.
	* @param  true or false.
	* @return NA.
	*/
	void EMChatConfigs::setAutoAcceptFriend(bool b) {
		getImpl()->setAutoAcceptFriend(b);
	}

	/**
	* \brief set if auto accept group invitation.
	*
	* Default is true.
	* @param  true or false.
	* @return NA.
	*/
	void EMChatConfigs::setAutoAcceptGroup(bool b) {
		getImpl()->setAutoAcceptGroup(b);
	}

	/**
	* \brief get if auto accept group invitation.
	*
	* @param  NA.
	* @return true or false.
	*/
	bool EMChatConfigs::getAutoAcceptGroup() {
		return getImpl()->getAutoAcceptGroup();
	}

	/**
	* \brief set if need message read ack.
	*
	* Default is true.
	* @param  true or false.
	* @return NA.
	*/
	void EMChatConfigs::setRequireReadAck(bool b) {
		getImpl()->setRequireReadAck(b);
	}

	/**
	* \brief get if need message read ack.
	*
	* @param  NA.
	* @return true or false.
	*/
	bool EMChatConfigs::getRequireReadAck() {
		return getImpl()->getRequireReadAck();
	}

	/**
	* \brief set if need message delivery ack.
	*
	* Default is false.
	* @param  true or false.
	* @return NA.
	*/
	void EMChatConfigs::setRequireDeliveryAck(bool b) {
		getImpl()->setRequireDeliveryAck(b);
	}

	/**
	* \brief get if need message delivery ack.
	*
	* @param  NA.
	* @return true or false.
	*/
	bool EMChatConfigs::getRequireDeliveryAck() {
		return getImpl()->getRequireDeliveryAck();
	}

	/**
	* \brief set if need load all conversation when login.
	*
	* Default is true.
	* @param  true or false.
	* @return NA.
	*/
	void EMChatConfigs::setAutoConversationLoaded(bool b) {
		getImpl()->setAutoConversationLoaded(b);
	}

	/**
	* \brief get if load all conversation when login.
	*
	* @param  NA.
	* @return true or false.
	*/
	bool EMChatConfigs::getAutoConversationLoaded() {
		return getImpl()->getAutoConversationLoaded();
	}

	/**
	* \brief set if delete message when exit group.
	*
	* Default is true.
	* @param  true or false.
	* @return NA.
	*/
	void EMChatConfigs::setDeleteMessageAsExitGroup(bool b) {
		getImpl()->setDeleteMessageAsExitGroup(b);
	}

	/**
	* \brief get if delete message when exit group.
	*
	* @param  NA.
	* @return true or false.
	*/
	bool EMChatConfigs::getDeleteMessageAsExitGroup() {
		return getImpl()->getDeleteMessageAsExitGroup();
	}

	/**
	* \brief set if chatroom owner can leave.
	*
	* Default is true.
	* @param  true or false.
	* @return NA.
	*/
	void EMChatConfigs::setIsChatroomOwnerLeaveAllowed(bool b) {
		getImpl()->setIsChatroomOwnerLeaveAllowed(b);
	}

	/**
	* \brief get if chatroom owner can leave.
	*
	* @param  NA.
	* @return true or false.
	*/
	bool EMChatConfigs::getIsChatroomOwnerLeaveAllowed() {
		return getImpl()->getIsChatroomOwnerLeaveAllowed();
	}

	/**
	* \brief set the number of message load at first time.
	*
	* Default is 20.
	* @param  true or false.
	* @return NA.
	*/
	void EMChatConfigs::setNumOfMessageLoaded(int n) {
		getImpl()->setNumOfMessageLoaded(n);
	}

	/**
	* \brief get the number of message load at first time.
	*
	* Default is 20.
	* @param
	* @return number of messages.
	*/
	int EMChatConfigs::getNumOfMessageLoaded() {
		return getImpl()->getNumOfMessageLoaded();
	}

	/**
	* \brief set os type.
	*
	* @param  os type.
	* @return NA.
	*/
	void EMChatConfigs::setOs(const OSType os) {
		getImpl()->setOs((easemob::EMChatConfigs::OSType)os);
	}

	/**
	* \brief get os type.
	*
	* @param  NA.
	* @return os type.
	*/
	OSType EMChatConfigs::getOs() {
		return (OSType)getImpl()->getOs();
	}

	/**
	* \brief set os version.
	*
	* @param  os version.
	* @return NA.
	*/
	void EMChatConfigs::setOsVersion(const String^ version) {
		getImpl()->setOsVersion(extractCSString(version));
	}

	/**
	* \brief get os version.
	*
	* @param  NA.
	* @return os version.
	*/
	const String^ EMChatConfigs::getOsVersion() {
		return getCSString(getImpl()->getOsVersion());
	}

	/**
	* \brief set sdk version.
	*
	* @param  sdk version.
	* @return NA.
	*/
	void EMChatConfigs::setSdkVersion(const String^ version) {
		getImpl()->setSdkVersion(extractCSString(version));
	}

	/**
	* \brief get sdk version.
	*
	* @param  NA.
	* @return sdk version.
	*/
	const String^ EMChatConfigs::getSdkVersion() {
		return getCSString(getImpl()->getSdkVersion());

	}

	/**
	* \brief get device unique id.
	*
	* @param  NA.
	* @return device unique id.
	*/
	unsigned int EMChatConfigs::getDeviceID() {
		return getImpl()->getDeviceID();
	}

	/**
	* \brief set client resource
	*
	* @param resource
	*/
	void EMChatConfigs::setClientResource(const String^ resource) {
		getImpl()->setClientResource(extractCSString(resource));
	}

	/**
	* \brief get client resource
	*
	* @return resource
	*/
	const String^ EMChatConfigs::clientResource() {
		return getCSString(getImpl()->clientResource());
	}

	/**
	* \brief Set log output level
	*
	* @param  log output level
	*/
	void EMChatConfigs::setLogLevel(EMLogLevel level) {
		getImpl()->setLogLevel((easemob::EMChatConfigs::EMLogLevel)level);
	}


	easemob::EMChatConfigsPtr& EMChatConfigs::getImpl() {
		return getNative<easemob::EMChatConfigsPtr>();
	}
}