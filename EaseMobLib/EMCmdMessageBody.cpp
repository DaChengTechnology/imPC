#include "pch.h"
#include "EMCmdMessageBody.h"
#include "EMUtils.h"

namespace EaseMobLib {
	/**
	* \brief Command message body constructor.
	*
	* @param  Command action
	* @param  Command parameters
	* @return NA
	*/
	EMCmdMessageBody::EMCmdMessageBody(const String^ action) : EMMessageBody(EMMessageBodyType::COMMAND) {
		easemob::EMCmdMessageBodyPtr body(new easemob::EMCmdMessageBody(extractCSString(action)));
		setNativeHandler<easemob::EMCmdMessageBodyPtr>(&body);
	}

	/**
	* \brief Copy constructor.
	*
	* @param  Another command message body.
	* @return NA
	*/
	EMCmdMessageBody::EMCmdMessageBody(const EMCmdMessageBody^ body) : EMMessageBody(EMMessageBodyType::COMMAND) {
		nativeInit<easemob::EMCmdMessageBodyPtr>(body);
	}

	/**
	* \brief Assign operator overload.
	*
	* @param  Another command message body.
	* @return The command message body.
	*/
	EMCmdMessageBody% EMCmdMessageBody::operator = (const EMCmdMessageBody% body) {
		nativeInit<easemob::EMCmdMessageBodyPtr>(% body);
		return *this;
	}

	/**
	* \brief Class destructor.
	*
	* @param  NA
	* @return NA
	*/
	EMCmdMessageBody::~EMCmdMessageBody() {
		nativeFinalize<easemob::EMCmdMessageBodyPtr>();
	}

	/**
	* \brief Get command action.
	*
	* @param  NA
	* @return The command action.
	*/
	const String^ EMCmdMessageBody::action() {
		return getCSString(getImpl()->action());
	}

	/**
	* \brief Set command action.
	*
	* @param  The command action.
	* @return NA
	*/
	void EMCmdMessageBody::setAction(const String^ action) {
		getImpl()->setAction(extractCSString(action));
	}

	/**
	* \brief Get command parameters.
	*
	* @param  NA
	* @return The command parameters.
	*/
	const EMCmdMessageBody::EMCmdParams^ EMCmdMessageBody::params() {
		easemob::EMCmdMessageBody::EMCmdParams params = getImpl()->params();
		cli::array<EMCmdParam^>^ csParams = gcnew cli::array<EMCmdParam^>(params.size());
		int i = 0;
		for (easemob::EMCmdMessageBody::EMCmdParam param : params) {
			EMCmdParam^ csParam = gcnew EMCmdParam;
			csParam->first = getCSString(param.first);
			csParam->second = getCSString(param.second);
			csParams[i] = csParam;
			i++;
		}
		return csParams;
	}

	/**
	* \brief Set command parameters.
	*
	* Note: User should not use command parameters any more, and use EMMessage's attribute instead.
	* @param  The command parameters.
	* @return NA
	*/
	void EMCmdMessageBody::setParams(const EMCmdMessageBody::EMCmdParams^ csParams) {
		easemob::EMCmdMessageBody::EMCmdParams params;
		for each (EMCmdParam ^ csParam in csParams) {
			params.push_back(easemob::EMCmdMessageBody::EMCmdParam(extractCSString(csParam->first), extractCSString(csParam->second)));
		}
		getImpl()->setParams(params);
	}

	easemob::EMCmdMessageBodyPtr& EMCmdMessageBody::getImpl() {
		return getNative<easemob::EMCmdMessageBodyPtr>();
	}
}