#include "pch.h"
#include <Windows.h>
#include <iostream>
#include <string>
#include <codecvt>
#include "EMUtils.h"
#include <vcclr.h>

using namespace std;
using namespace System;

namespace EaseMobLib {
	void Wchar_tToString(std::string& szDst, wchar_t* wchar)
	{
		wchar_t* wText = wchar;
		DWORD dwNum = WideCharToMultiByte(CP_OEMCP, NULL, wText, -1, NULL, 0, NULL, FALSE);// WideCharToMultiByte������
		char* psText;  // psTextΪchar*����ʱ���飬��Ϊ��ֵ��std::string���м�����
		psText = new char[dwNum];
		WideCharToMultiByte(CP_OEMCP, NULL, wText, -1, psText, dwNum, NULL, FALSE);// WideCharToMultiByte���ٴ�����
		szDst = psText;// std::string��ֵ
		delete[]psText;// psText������
	}

	// string to wstring
	void StringToWstring(std::wstring& szDst, std::string str)
	{
		std::string temp = str;
		int len = MultiByteToWideChar(CP_ACP, 0, (LPCSTR)temp.c_str(), -1, NULL, 0);
		wchar_t* wszUtf8 = new wchar_t[len + 1];
		memset(wszUtf8, 0, len * 2 + 2);
		MultiByteToWideChar(CP_ACP, 0, (LPCSTR)temp.c_str(), -1, (LPWSTR)wszUtf8, len);
		szDst = wszUtf8;
		std::wstring r = wszUtf8;
		delete[] wszUtf8;
	}

	std::wstring utf8_to_wstring(const std::string& str) {
		std::wstring_convert<std::codecvt_utf8<wchar_t> > mconv;
		return mconv.from_bytes(str);
	}

	std::string wstring_to_utf8(const std::wstring& str) {
		std::wstring_convert<std::codecvt_utf8<wchar_t> > mconv;
		return mconv.to_bytes(str);
	}

	String^ getCSString(const std::string& str)
	{
		std::wstring wstr = utf8_to_wstring(str);
		return gcnew String(wstr.c_str());
	}

	std::string extractCSString(const String^ str) {
		if (str == nullptr) {
			return "";
		}
		pin_ptr<const WCHAR> wch = PtrToStringChars(str);
		return wstring_to_utf8(wch);
	}

	Dictionary<String^, int64_t>^ fillCSDictionary(const std::vector<std::pair<std::string, int64_t> >& mutes) {
		Dictionary<String^, int64_t>^ csDict = gcnew Dictionary<String^, int64_t>();
		for (int i = 0; i < mutes.size(); i++) {
			csDict->Add(getCSString(mutes[i].first), mutes[i].second);
		}
		return csDict;
	}
	/*
	cli::array< String^>^ fillCSArray(std::vector<std::string>& list) {
		cli::array<const String^>^ csArray = gcnew cli::array<const String^>(list.size());
		for (int i = 0; i < list.size(); i++) {
			csArray[i] = getCSString(list[i]);
		}
		return csArray;
	}*/

	cli::array<String^>^ fillCSArray(std::vector<std::string>& list) {
		cli::array<String^>^ csArray = gcnew cli::array<String^>(list.size());
		for (int i = 0; i < list.size(); i++) {
			csArray[i] = getCSString(list[i]);
		}
		return csArray;
	}

	
	/*generic <typename T>
		cli::array<T>^ fillCSArray(std::vector<T> &vec) {
			cli::array<Type ^>^ csArray = gcnew cli::array<Type ^>(vec.size());
		for (int i = 0; i < vec.size(); i++) {
			csArray[i] = vec[i];
		}
		return csArray;
	}*/

	/*generic <typename T>
		cli::array<T >^ fillCSArray(const std::list<Type^> &vec) {
			cli::array<T>^ csArray = gcnew cli::array<T>(vec.size());
		for (int i = 0; i < vec.size(); i++) {
			csArray[i] = vec[i];
		}
		return csArray;
	}*/

	void extractArray(cli::array<String^>^ arr, std::vector<std::string>& v) {
		v.clear();
		for each (String ^ str in arr) {
			v.push_back(extractCSString(str));
		}
	}

	void extractArray(cli::array<String^>^ arr, std::list<std::string>& l) {
		l.clear();
		for each (String ^ str in arr) {
			l.push_back(extractCSString(str));
		}
	}


	EMError^ getCSError(const easemob::EMErrorPtr* ptr) {
		EMError^ csError = gcnew EMError;
		csError->setNativeHandler<easemob::EMErrorPtr>(ptr);
		return csError;
	}

	EMError^ getCSError(const easemob::EMErrorPtr& err) {
		EMError^ csError = gcnew EMError;
		csError->setNativeHandler<easemob::EMErrorPtr>(&err);
		return csError;
	}

	EMMessage^ getCSMessage(const easemob::EMMessagePtr* ptr) {
		EMMessage^ csMessage = gcnew EMMessage;
		csMessage->setNativeHandler<easemob::EMMessagePtr>(ptr);
		return csMessage;
	}

	EMMessage^ getCSMessage(const easemob::EMMessagePtr& msg) {
		EMMessage^ csMessage = gcnew EMMessage;
		csMessage->setNativeHandler<easemob::EMMessagePtr>(&msg);
		return csMessage;
	}

	EMTextMessageBody^ getCSTextMessageBody(const easemob::EMTextMessageBodyPtr* ptr) {
		EMTextMessageBody^ csBody = gcnew EMTextMessageBody("");
		csBody->setNativeHandler<easemob::EMTextMessageBodyPtr>(ptr);
		return csBody;
	}

	EMImageMessageBody^ getCSImageMessageBody(const easemob::EMImageMessageBodyPtr* ptr) {
		EMImageMessageBody^ csBody = gcnew EMImageMessageBody();
		csBody->setNativeHandler<easemob::EMImageMessageBodyPtr>(ptr);
		return csBody;
	}

	EMLocationMessageBody^ getCSLocationMessageBody(const easemob::EMLocationMessageBodyPtr* ptr) {
		EMLocationMessageBody^ csBody = gcnew EMLocationMessageBody(0.0f, 0.0f, "");
		csBody->setNativeHandler<easemob::EMLocationMessageBodyPtr>(ptr);
		return csBody;
	}

	EMFileMessageBody^ getCSFileMessageBody(const easemob::EMFileMessageBodyPtr* ptr) {
		EMFileMessageBody^ csBody = gcnew EMFileMessageBody(EMMessageBodyType::FILE);
		csBody->setNativeHandler<easemob::EMFileMessageBodyPtr>(ptr);
		return csBody;
	}

	EMVideoMessageBody^ getCSVideoMessageBody(const easemob::EMVideoMessageBodyPtr* ptr) {
		EMVideoMessageBody^ csBody = gcnew EMVideoMessageBody();
		csBody->setNativeHandler<easemob::EMVideoMessageBodyPtr>(ptr);
		return csBody;
	}

	EMVoiceMessageBody^ getCSVoiceMessageBody(const easemob::EMVoiceMessageBodyPtr* ptr) {
		EMVoiceMessageBody^ csBody = gcnew EMVoiceMessageBody();
		csBody->setNativeHandler<easemob::EMVoiceMessageBodyPtr>(ptr);
		return csBody;
	}

	EMCmdMessageBody^ getCSCmdMessageBody(const easemob::EMCmdMessageBodyPtr* ptr) {
		EMCmdMessageBody^ csBody = gcnew EMCmdMessageBody("");
		csBody->setNativeHandler<easemob::EMCmdMessageBodyPtr>(ptr);
		return csBody;
	}

	EMTextMessageBody^ getCSTextMessageBody(const easemob::EMTextMessageBodyPtr& _ptr) {
		EMTextMessageBody^ csBody = gcnew EMTextMessageBody("");
		csBody->setNativeHandler<easemob::EMTextMessageBodyPtr>(&_ptr);
		return csBody;
	}

	EMImageMessageBody^ getCSImageMessageBody(const easemob::EMImageMessageBodyPtr& _ptr) {
		EMImageMessageBody^ csBody = gcnew EMImageMessageBody();
		csBody->setNativeHandler<easemob::EMImageMessageBodyPtr>(&_ptr);
		return csBody;
	}

	EMLocationMessageBody^ getCSLocationMessageBody(const easemob::EMLocationMessageBodyPtr& _ptr) {
		EMLocationMessageBody^ csBody = gcnew EMLocationMessageBody(0.0f, 0.0f, "");
		csBody->setNativeHandler<easemob::EMLocationMessageBodyPtr>(&_ptr);
		return csBody;
	}

	EMFileMessageBody^ getCSFileMessageBody(const easemob::EMFileMessageBodyPtr& _ptr) {
		EMFileMessageBody^ csBody = gcnew EMFileMessageBody(EMMessageBodyType::FILE);
		csBody->setNativeHandler<easemob::EMFileMessageBodyPtr>(&_ptr);
		return csBody;
	}

	EMVideoMessageBody^ getCSVideoMessageBody(const easemob::EMVideoMessageBodyPtr& _ptr) {
		EMVideoMessageBody^ csBody = gcnew EMVideoMessageBody();
		csBody->setNativeHandler<easemob::EMVideoMessageBodyPtr>(&_ptr);
		return csBody;
	}

	EMVoiceMessageBody^ getCSVoiceMessageBody(const easemob::EMVoiceMessageBodyPtr& _ptr) {
		EMVoiceMessageBody^ csBody = gcnew EMVoiceMessageBody();
		csBody->setNativeHandler<easemob::EMVoiceMessageBodyPtr>(&_ptr);
		return csBody;
	}

	EMCmdMessageBody^ getCSCmdMessageBody(const easemob::EMCmdMessageBodyPtr& _ptr) {
		EMCmdMessageBody^ csBody = gcnew EMCmdMessageBody("");
		csBody->setNativeHandler<easemob::EMCmdMessageBodyPtr>(&_ptr);
		return csBody;
	}

	EMConversation^ getCSConversation(const easemob::EMConversationPtr* ptr) {
		EMConversation^ csConversation = gcnew EMConversation();
		csConversation->setNativeHandler<easemob::EMConversationPtr>(ptr);
		return csConversation;
	}

	EMGroup^ getCSGroup(const easemob::EMGroupPtr* ptr) {
		EMGroup^ csGroup = gcnew EMGroup();
		csGroup->setNativeHandler<easemob::EMGroupPtr>(ptr);
		return csGroup;
	}

	EMConversation^ getCSConversation(const easemob::EMConversationPtr& _ptr) {
		EMConversation^ csConversation = gcnew EMConversation();
		csConversation->setNativeHandler<easemob::EMConversationPtr>(&_ptr);
		return csConversation;
	}

	EMGroup^ getCSGroup(const easemob::EMGroupPtr& _ptr) {
		EMGroup^ csGroup = gcnew EMGroup();
		csGroup->setNativeHandler<easemob::EMGroupPtr>(&_ptr);
		return csGroup;
	}

	/*
	EMCallBack ^getCSCallback(const easemob::EMCallbackPtr* ptr) {
		EMCallBack ^csCallback = gcnew EMCallBack();
		csCallback->setNativeHandler<easemob::EMCallback>(ptr);
		return csCallback;
	}
	*/

	cli::array<EaseMobLib::EMMessage^>^ fillCSMessageList(const easemob::EMMessageList& list) {
		cli::array<EaseMobLib::EMMessage^>^ csList = gcnew cli::array<EaseMobLib::EMMessage^>(list.size());
		int i = 0;
		for (const easemob::EMMessagePtr msg : list) {
			EaseMobLib::EMMessage^ csMsg = getCSMessage(msg);
			csList[i] = csMsg;
			i++;
		}
		return csList;
	}

	cli::array<EaseMobLib::EMConversation^>^ fillCSConversationList(const easemob::EMConversationList& list) {
		cli::array<EaseMobLib::EMConversation^>^ csList = gcnew cli::array<EaseMobLib::EMConversation^>(list.size());
		int i = 0;
		for (const easemob::EMConversationPtr msg : list) {
			EaseMobLib::EMConversation^ csConversation = getCSConversation(msg);
			csList[i] = csConversation;
			i++;
		}
		return csList;
	}
}
