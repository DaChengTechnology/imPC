#include "pch.h"
#include "EMImageMessageBody.h"
#include "EMUtils.h"

namespace EaseMobLib {
	/**
	* \brief Image message body constructor.
	*
	* @param  NA
	* @return NA
	*/
	EMImageMessageBody::EMImageMessageBody() : EMFileMessageBody(EMMessageBodyType::IMAGE) {

	}

	/**
	* \brief Image message body constructor.
	*
	* @param  Image attachment local path.
	* @param  Image thumbnail local path.
	* @return NA
	*/
	EMImageMessageBody::EMImageMessageBody(const String^ localPath, const String^ thumbnailLocalPath)
		: EMFileMessageBody(EMMessageBodyType::IMAGE) {
		easemob::EMImageMessageBodyPtr body(new easemob::EMImageMessageBody(extractCSString(localPath), extractCSString(thumbnailLocalPath)));
		setNativeHandler<easemob::EMImageMessageBodyPtr>(&body);
	}

	/**
	* \brief Copy constructor.
	*
	* Note: Only copy the file path.
	* @param  Another image message body.
	* @return NA
	*/
	EMImageMessageBody::EMImageMessageBody(const EMImageMessageBody% body)
		: EMFileMessageBody(EMMessageBodyType::IMAGE) {
		nativeInit<easemob::EMImageMessageBodyPtr>(% body);
	}

	/**
	* \brief Assign operator overload.
	*
	* Note: Only copy the file path.
	* @param  Another image message body.
	* @return The image message body.
	*/
	EMImageMessageBody% EMImageMessageBody::operator=(const EMImageMessageBody% body) {
		nativeInit<easemob::EMImageMessageBodyPtr>(% body);
		return *this;
	}

	/**
	* \brief Class destructor.
	*
	* @param  NA
	* @return NA
	*/
	EMImageMessageBody::~EMImageMessageBody() {
		nativeFinalize<easemob::EMImageMessageBodyPtr>();
	}

	/**
	* \brief Set display name of the thumbnail.
	*
	* @param  The display name.
	* @return NA
	*/
	void EMImageMessageBody::setThumbnailDisplayName(const String^ displayName) {
		getImpl()->setThumbnailDisplayName(extractCSString(displayName));
	}

	/**
	* \brief Get display name of the thumbnail.
	*
	* @param  NA
	* @return The display name.
	*/
	const String^ EMImageMessageBody::thumbnailDisplayName() {
		return getCSString(getImpl()->thumbnailDisplayName());
	}

	/**
	* \brief Set local path of the thumbnail.
	*
	* @param  The local path.
	* @return NA
	*/
	void EMImageMessageBody::setThumbnailLocalPath(const String^ localPath) {
		getImpl()->setThumbnailLocalPath(extractCSString(localPath));
	}

	/**
	* \brief Get local path of the thumbnail.
	*
	* @param  NA
	* @return The local path.
	*/
	const String^ EMImageMessageBody::thumbnailLocalPath() {
		return getCSString(getImpl()->thumbnailLocalPath());
	}

	/**
	* \brief Set remote path of the thumbnail.
	*
	* Note: It's internal used, user should never need to call this method.
	* @param  The remote path.
	* @return NA
	*/
	void EMImageMessageBody::setThumbnailRemotePath(const String^ remotePath) {
		getImpl()->setThumbnailRemotePath(extractCSString(remotePath));
	}

	/**
	* \brief Get remote path of the thumbnail.
	*
	* @param  NA
	* @return The remote path.
	*/
	const String^ EMImageMessageBody::thumbnailRemotePath() {
		return getCSString(getImpl()->thumbnailRemotePath());
	}

	/**
	* \brief Set secret key of the thumbnail.
	*
	* Note: It's internal used, user should never need to call this method.
	* @param  The secret key.
	* @return NA
	*/
	void EMImageMessageBody::setThumbnailSecretKey(const String^ secretKey) {
		getImpl()->setThumbnailSecretKey(extractCSString(secretKey));
	}

	/**
	* \brief Get secret key of the thumbnail.
	*
	* @param  NA
	* @return The secret key.
	*/
	const String^ EMImageMessageBody::thumbnailSecretKey() {
		return getCSString(getImpl()->thumbnailSecretKey());
	}

	/**
	* \brief Set size of the thumbnail.
	*
	* @param  The thumbnail size.
	* @return NA
	*/
	void EMImageMessageBody::setThumbnailSize(const Size^ size) {
		getImpl()->setThumbnailSize(easemob::EMImageMessageBody::Size(size->mWidth, size->mHeight));
	}

	/**
	* \brief Get size of the thumbnail.
	*
	* @param  NA
	* @return The thumbnail size.
	*/
	EMImageMessageBody::Size^ EMImageMessageBody::thumbnailSize() {
		easemob::EMImageMessageBody::Size size = getImpl()->thumbnailSize();
		return gcnew Size(size.mWidth, size.mHeight);
	}

	/**
	* \brief Set file length of the thumbnail.
	*
	* Note: It's usually not necessary to call this method, will calculate file length automatically when setting local path.
	* @param  The file length.
	* @return NA
	*/
	void EMImageMessageBody::setThumbnailFileLength(int64_t fileLength) {
		getImpl()->setThumbnailFileLength(fileLength);
	}

	/**
	* \brief Get file length of the thumbnail.
	*
	* @param  NA
	* @return The file length.
	*/
	int64_t EMImageMessageBody::thumbnailFileLength() {
		return getImpl()->thumbnailFileLength();
	}

	/**
	* \brief Set download status of the thumbnail.
	*
	* Note: Usually, user should NOT call this method directly.
	* @param  The download status.
	* @return NA
	*/
	void EMImageMessageBody::setThumbnailDownloadStatus(EMDownloadStatus status) {
		getImpl()->setThumbnailDownloadStatus((easemob::EMFileMessageBody::EMDownloadStatus)status);
	}

	/**
	* \brief Get download status of the thumbnail.
	*
	* @param  NA
	* @return The download status.
	*/
	EMDownloadStatus EMImageMessageBody::thumbnailDownloadStatus() {
		return (EMDownloadStatus)getImpl()->thumbnailDownloadStatus();
	}

	/**
	* \brief Set size of the image.
	*
	* @param  The image's size.
	* @return NA
	*/
	void EMImageMessageBody::setSize(const Size^ size) {
		getImpl()->setSize(easemob::EMImageMessageBody::Size(size->mWidth, size->mHeight));
	}

	/**
	* \brief Get size of the image.
	*
	* @param  NA
	* @return The image size.
	*/
	const EMImageMessageBody::Size^ EMImageMessageBody::size() {
		easemob::EMImageMessageBody::Size size = getImpl()->size();
		return gcnew Size(size.mWidth, size.mHeight);
	}

	easemob::EMImageMessageBodyPtr& EMImageMessageBody::getImpl() {
		return getNative<easemob::EMImageMessageBodyPtr>();
	}
}