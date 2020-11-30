#pragma once
#include <string>
#include <vector>
#include <list>

#include"EMMessageBody.h"
#include "EMCmdMessageBody.h"
#include "EMTextMessageBody.h"
#include "EMFileMessageBody.h"
#include "EMLocationMessageBody.h"
#include "EMImageMessageBody.h"
#include "EMVideoMessageBody.h"
#include "EMVoiceMessageBody.h"
#include "EMMessage.h"
#include "EMConversation.h"
#include "EMGroup.h"

using namespace System;
using namespace System::Collections::Generic;

namespace EaseMobLib {
	void Wchar_tToString(std::string& szDst, wchar_t* wchar);

	// string to wstring
	void StringToWstring(std::wstring& szDst, std::string str);

	std::wstring utf8_to_wstring(const std::string& str);

	std::string wstring_to_utf8(const std::wstring& str);

	String^ getCSString(const std::string&);
	std::string extractCSString(const String^);

	Dictionary<String^, int64_t>^ fillCSDictionary(const std::vector<std::pair<std::string, int64_t> >&);
	cli::array<const String^>^ fillCSArray(const std::vector<std::string>&);

	
	/*generic <typename T>
	cli::array<T>^ fillCSArray(std::vector<T> &);
	/*
	generic <typename T>
	array<T ^>^ fillCSArray(const std::list<T^> &);
	*/

	void extractArray(cli::array<String^>^ arr, std::vector<std::string>& l);
	void extractArray(cli::array<String^>^ arr, std::list<std::string>& l);

	EMError^ getCSError(const easemob::EMErrorPtr*);
	EMError^ getCSError(const easemob::EMErrorPtr&);
	EMMessage^ getCSMessage(const easemob::EMMessagePtr*);
	EMMessage^ getCSMessage(const easemob::EMMessagePtr&);

	EMTextMessageBody^ getCSTextMessageBody(const easemob::EMTextMessageBodyPtr*);
	EMImageMessageBody^ getCSImageMessageBody(const easemob::EMImageMessageBodyPtr*);
	EMLocationMessageBody^ getCSLocationMessageBody(const easemob::EMLocationMessageBodyPtr*);
	EMFileMessageBody^ getCSFileMessageBody(const easemob::EMFileMessageBodyPtr*);
	EMVideoMessageBody^ getCSVideoMessageBody(const easemob::EMVideoMessageBodyPtr*);
	EMVoiceMessageBody^ getCSVoiceMessageBody(const easemob::EMVoiceMessageBodyPtr*);
	EMCmdMessageBody^ getCSCmdMessageBody(const easemob::EMCmdMessageBodyPtr*);

	EMTextMessageBody^ getCSTextMessageBody(const easemob::EMTextMessageBodyPtr&);
	EMImageMessageBody^ getCSImageMessageBody(const easemob::EMImageMessageBodyPtr&);
	EMLocationMessageBody^ getCSLocationMessageBody(const easemob::EMLocationMessageBodyPtr&);
	EMFileMessageBody^ getCSFileMessageBody(const easemob::EMFileMessageBodyPtr&);
	EMVideoMessageBody^ getCSVideoMessageBody(const easemob::EMVideoMessageBodyPtr&);
	EMVoiceMessageBody^ getCSVoiceMessageBody(const easemob::EMVoiceMessageBodyPtr&);
	EMCmdMessageBody^ getCSCmdMessageBody(const easemob::EMCmdMessageBodyPtr&);


	EMConversation^ getCSConversation(const easemob::EMConversationPtr*);
	EMGroup^ getCSGroup(const easemob::EMGroupPtr*);
	EMCallback^ getCSCallback(const easemob::EMCallbackPtr*);


	EMConversation^ getCSConversation(const easemob::EMConversationPtr&);
	EMGroup^ getCSGroup(const easemob::EMGroupPtr&);
	EMCallback^ getCSCallback(const easemob::EMCallbackPtr&);


	cli::array<EMMessage^>^ fillCSMessageList(const easemob::EMMessageList& list);
	cli::array<EMConversation^>^ fillCSConversationList(const easemob::EMConversationList& list);
}

