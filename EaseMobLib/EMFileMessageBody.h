#pragma once
#include "include/message/emfilemessagebody.h"

#include "EMMessageBody.h"
using namespace System;

namespace EaseMobLib {
	/**
	* Download status of file attachment.
	*/
	public enum class EMDownloadStatus
	{
		DOWNLOADING,    //Download is in progress.
		SUCCESSED,      //Download successed.
		FAILED,         //Download failed.
		PENDING         //Download has not begun.
	};
	public ref class EMFileMessageBody:EMMessageBody
	{
	public:

		/**
		* \brief File message body constructor.
		*
		* @param  Attachment file type.
		* @return NA
		*/
		EMFileMessageBody(EMMessageBodyType type);

		/**
		* \brief File message body constructor.
		*
		* @param  Attachment local path.
		* @param  Attachment type
		* @return NA
		*/
		EMFileMessageBody(const String^ localPath, EMMessageBodyType type);

		/**
		* \brief Copy constructor.
		*
		* Note: Only copy the file path.
		* @param  Another file message body.
		* @return NA
		*/
		EMFileMessageBody(const EMFileMessageBody%);

		/**
		* \brief Assign operator overload.
		*
		* Note: Only copy the file path.
		* @param  Another file message body.
		* @return The file message body.
		*/
		EMFileMessageBody% operator=(const EMFileMessageBody%);

		/**
		* \brief Class destructor.
		*
		* @param  NA
		* @return NA
		*/
		virtual ~EMFileMessageBody();

		/**
		* \brief Get display name of the attachment.
		*
		* @param  NA
		* @return The display name.
		*/
		String^ displayName();

		/**
		* \brief Set display name of the attachment.
		*
		* @param  The display name.
		* @return NA
		*/
		void setDisplayName(const String^);

		/**
		* \brief Get local path of the attachment.
		*
		* @param  NA
		* @return The local path.
		*/
		const String^ localPath();

		/**
		* \brief Set local path of the attachment.
		*
		* Note: Received meesage should NOT change the local path.
		* @param  The local path.
		* @return NA
		*/
		void setLocalPath(const String^);

		/**
		* \brief Get remote path of the attachment.
		*
		* @param  NA
		* @return The remote path.
		*/
		const String^ remotePath();

		/**
		* \brief Set remote path of the attachment.
		*
		* Note: It's internal used, user should never need to call this method.
		* @param  The remote path.
		* @return NA
		*/
		void setRemotePath(const String^);

		/**
		* \brief Get secret key of the attachment, it's used to download attachment from server.
		*
		* @param  NA
		* @return The secret key.
		*/
		const String^ secretKey();

		/**
		* \brief Set secret key of the attachment.
		*
		* Note: It's internal used, user should never need to call this method.
		* @param  The secret key.
		* @return NA
		*/
		void setSecretKey(const String^);

		/**
		* \brief Get file length of the attachment.
		*
		* @param  NA
		* @return The file length.
		*/
		Int64 fileLength();

		/**
		* \brief Set file length of the attachment.
		*
		* Note: It's usually not necessary to call this method, will calculate file length automatically when setting local path.
		* @param  The file length.
		* @return NA
		*/
		void setFileLength(Int64);

		/**
		* \brief Get file download status of the attachment.
		*
		* @param  NA
		* @return The file download status.
		*/
		EMDownloadStatus downloadStatus();

		/**
		* \brief Set download status of the attachment.
		*
		* Note: Usually, user should NOT call this method directly.
		* @param  The download status.
		* @return NA
		*/
		void setDownloadStatus(EMDownloadStatus);

	private:
		easemob::EMFileMessageBodyPtr& EMFileMessageBody::getImpl();

	};
}

