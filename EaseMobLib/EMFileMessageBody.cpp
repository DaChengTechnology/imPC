#include "pch.h"

#include "include/message/emcmdmessagebody.h"

#include "EMFileMessageBody.h"
#include "EMUtils.h"

namespace EaseMobLib {
	/**
	* \brief File message body constructor.
	*
	* @param  Attachment file type.
	* @return NA
	*/
	EMFileMessageBody::EMFileMessageBody(EMMessageBodyType type) : EMMessageBody(type) {
		EMFileMessageBody(getCSString(""), type);
	}

	/**
	* \brief File message body constructor.
	*
	* @param  Attachment local path.
	* @param  Attachment type
	* @return NA
	*/
	EMFileMessageBody::EMFileMessageBody(const String^ localPath, EMMessageBodyType type) : EMMessageBody(type) {
		easemob::EMFileMessageBodyPtr body(new easemob::EMFileMessageBody(extractCSString(localPath), (easemob::EMMessageBody::EMMessageBodyType)type));
		setNativeHandler<easemob::EMCmdMessageBodyPtr>(&body);
	}

	/**
	* \brief Copy constructor.
	*
	* Note: Only copy the file path.
	* @param  Another file message body.
	* @return NA
	*/
	EMFileMessageBody::EMFileMessageBody(const EMFileMessageBody% body) : EMMessageBody(EMMessageBodyType::FILE) {
		nativeInit<easemob::EMFileMessageBodyPtr>(% body);
	}

	/**
	* \brief Assign operator overload.
	*
	* Note: Only copy the file path.
	* @param  Another file message body.
	* @return The file message body.
	*/
	EMFileMessageBody% EMFileMessageBody::operator = (const EMFileMessageBody% body) {
		nativeInit<easemob::EMFileMessageBodyPtr>(% body);
		return *this;
	}

	/**
	* \brief Class destructor.
	*
	* @param  NA
	* @return NA
	*/
	EMFileMessageBody::~EMFileMessageBody() {
		nativeFinalize<easemob::EMFileMessageBodyPtr>();
	}

	/**
	* \brief Get display name of the attachment.
	*
	* @param  NA
	* @return The display name.
	*/
	String^ EMFileMessageBody::displayName() {
		return getCSString(getImpl()->displayName());
	}

	/**
	* \brief Set display name of the attachment.
	*
	* @param  The display name.
	* @return NA
	*/
	void EMFileMessageBody::setDisplayName(const String^ displayName) {
		getImpl()->setDisplayName(extractCSString(displayName));
	}

	/**
	* \brief Get local path of the attachment.
	*
	* @param  NA
	* @return The local path.
	*/
	const String^ EMFileMessageBody::localPath() {
		return getCSString(getImpl()->localPath());
	}

	/**
	* \brief Set local path of the attachment.
	*
	* Note: Received meesage should NOT change the local path.
	* @param  The local path.
	* @return NA
	*/
	void EMFileMessageBody::setLocalPath(const String^ localPath) {
		getImpl()->setLocalPath(extractCSString(localPath));
	}

	/**
	* \brief Get remote path of the attachment.
	*
	* @param  NA
	* @return The remote path.
	*/
	const String^ EMFileMessageBody::remotePath() {
		return getCSString(getImpl()->remotePath());
	}

	/**
	* \brief Set remote path of the attachment.
	*
	* Note: It's internal used, user should never need to call this method.
	* @param  The remote path.
	* @return NA
	*/
	void EMFileMessageBody::setRemotePath(const String^ remotePath) {
		getImpl()->setRemotePath(extractCSString(remotePath));
	}

	/**
	* \brief Get secret key of the attachment, it's used to download attachment from server.
	*
	* @param  NA
	* @return The secret key.
	*/
	const String^ EMFileMessageBody::secretKey() {
		return getCSString(getImpl()->secretKey());
	}

	/**
	* \brief Set secret key of the attachment.
	*
	* Note: It's internal used, user should never need to call this method.
	* @param  The secret key.
	* @return NA
	*/
	void EMFileMessageBody::setSecretKey(const String^ secretKey) {
		getImpl()->setSecretKey(extractCSString(secretKey));
	}

	/**
	* \brief Get file length of the attachment.
	*
	* @param  NA
	* @return The file length.
	*/
	// TODO: Ô­À´ÊÇint64
	Int64 EMFileMessageBody::fileLength() {
		return getImpl()->fileLength();
	}

	/**
	* \brief Set file length of the attachment.
	*
	* Note: It's usually not necessary to call this method, will calculate file length automatically when setting local path.
	* @param  The file length.
	* @return NA
	*/
	void EMFileMessageBody::setFileLength(Int64 length) {
		getImpl()->setFileLength(length);
	}

	/**
	* \brief Get file download status of the attachment.
	*
	* @param  NA
	* @return The file download status.
	*/
	EMDownloadStatus EMFileMessageBody::downloadStatus() {
		return (EMDownloadStatus)getImpl()->downloadStatus();
	}

	/**
	* \brief Set download status of the attachment.
	*
	* Note: Usually, user should NOT call this method directly.
	* @param  The download status.
	* @return NA
	*/
	void EMFileMessageBody::setDownloadStatus(EMDownloadStatus status) {
		getImpl()->setDownloadStatus((easemob::EMFileMessageBody::EMDownloadStatus)status);
	}

	easemob::EMFileMessageBodyPtr& EMFileMessageBody::getImpl() {
		return getNative<easemob::EMFileMessageBodyPtr>();
	}
}