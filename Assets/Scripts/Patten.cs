using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml;

public class Patten : MonoBehaviour {
	private string[] _items1_1 = {"everything", "sth", "avg", "self"};
	
	private string[] _items2_1 = {"buff", "debuff"};
	private string[] _items2_2 = {"hp", "mp", "attackgauge", "stet", "buff", "debuff"};
	private string[] _items2_3 = {"hp", "mp", "attackgauge", "stet", "buff", "debuff"};
	private string[] _items2_4 = {"hp", "mp", "attackgauge", "stet", "buff", "debuff"};
	
	private string[] _items2_1_1 = {"catch", "miss"};
	private string[] _items2_1_2 = {"catch", "miss"};
	
	private string[] _items2_2_1 = {"+above", "+below", "%above", "%below", "best", "worst"};
	private string[] _items2_2_2 = {"+above", "+below", "%above", "%below", "best", "worst"};
	private string[] _items2_2_3 = {"+above", "+below", "%above", "%below", "best", "worst"};
	private string[] _items2_2_4 = {"+above", "+below", "%above", "%below", "best", "worst"};
	private string[] _items2_2_5 = {"catch", "miss"};
	private string[] _items2_2_6 = {"catch", "miss"};
	
	private string[] _items2_3_1 = {"+above", "+below", "%above", "%below", "best", "worst"};
	private string[] _items2_3_2 = {"+above", "+below", "%above", "%below", "best", "worst"};
	private string[] _items2_3_3 = {"+above", "+below", "%above", "%below", "best", "worst"};
	private string[] _items2_3_4 = {"+above", "+below", "%above", "%below", "best", "worst"};
	private string[] _items2_3_5 = {"catch", "miss"};
	private string[] _items2_3_6 = {"catch", "miss"};
	
	private string[] _items2_4_1 = {"+above", "+below", "%above", "%below", "best", "worst"};
	private string[] _items2_4_2 = {"+above", "+below", "%above", "%below", "best", "worst"};
	private string[] _items2_4_3 = {"+above", "+below", "%above", "%below", "best", "worst"};
	private string[] _items2_4_4 = {"+above", "+below", "%above", "%below", "best", "worst"};
	private string[] _items2_4_5 = {"catch", "miss"};
	private string[] _items2_4_6 = {"catch", "miss"};
	
	private string _selectedItem1 = "select";
	private string _selectedItem2 = "select";
	private string _selectedItem3 = "select";
	private string _saveItem = "save";
	
	private bool _editing1 = false;
	private bool _editing2 = false;
	private bool _editing3 = false;
	private bool _editing4 = false;
	
	private bool _select1 = false;
	private bool _select2 = false;
	
	private float _screenWidth;
	private float _screenHeight;

	//
	private string patturn1;
	private string patturn2;
	private string patturn3;

	
	private void OnGUI() {
		_screenWidth = Screen.width;
		_screenHeight = Screen.height;
		
		if (GUI.Button (new Rect (_screenWidth/6.5f, _screenHeight/3.2f, 80, 20), _selectedItem1)) {
			_editing1 = true;
		}
		if (GUI.Button (new Rect (_screenWidth/2.5f, _screenHeight/3.2f, 80, 20), _selectedItem2)) {
			_editing2 = true;
		}
		if (GUI.Button (new Rect (_screenWidth/1.5f, _screenHeight/3.2f, 80, 20), _selectedItem3)) {
			_editing3 = true;
		}
		if (GUI.Button (new Rect (_screenWidth/1.5f, _screenHeight/6.0f, 80, 20), _saveItem)) {
			_editing4 = true;
        }
		if(_editing1){
			for(int x = 0; x < _items1_1.Length; x++) {
				if(GUI.Button (new Rect(_screenWidth/6.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items1_1[x])) {
					_selectedItem1 = _items1_1[x];
                    patturn1 = System.Convert.ToString(x + 1);
					_selectedItem2 = "select";
					_selectedItem3 = "select";
					_editing1 = false;
					_select1 = true;

				}
			}
		}
		if(_select1) {
			switch(_selectedItem1) {
			case "everything" :
				if(_editing2){
					for(int x = 0; x < _items2_1.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/2.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_1[x])) {
							_selectedItem2 = _items2_1[x];
                            patturn2 = System.Convert.ToString(x + 1);
							_selectedItem3 = "select";
							_editing2 = false;
							_select2 = true;
						}
					}
					//
					patturn1 = "1";
				}
				break;
			case "sth" :
				if(_editing2){
					for(int x = 0; x < _items2_2.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/2.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_2[x])) {
                            _selectedItem2 = _items2_2[x];
                            patturn2 = System.Convert.ToString(x + 1);
							_selectedItem3 = "select";
							_editing2 = false;
							_select2 = true;
						}
					}
				}
				break;
			case "avg" :
				if(_editing2){
					for(int x = 0; x < _items2_3.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/2.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_3[x])) {
                            _selectedItem2 = _items2_3[x];
                            patturn2 = System.Convert.ToString(x + 1);
							_selectedItem3 = "select";
							_editing2 = false;
							_select2 = true;
						}
					}
					//
				}
				break;
			case "self" :
				if(_editing2){
					for(int x = 0; x < _items2_4.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/2.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_4[x])) {
                            _selectedItem2 = _items2_4[x];
                            patturn2 = System.Convert.ToString(x + 1);
							_selectedItem3 = "select";
							_editing2 = false;
							_select2 = true;
						}
					}
					//
				}
				break;
			}
		}
		
		
		if(_select2 &&_selectedItem1 == "everything") {
			switch(_selectedItem2){
			case "buff" :
				if(_editing3){
                    
					for(int x = 0; x < _items2_1_1.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/1.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_1_1[x])) {
                            _selectedItem3 = _items2_1_1[x];
                            patturn3 = System.Convert.ToString(x + 1);
							_editing3 = false;
						}
					}
					//
				}
				break;
			case "debuff" :
				if(_editing3){
					for(int x = 0; x < _items2_1_2.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/1.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_1_2[x])) {
                            _selectedItem3 = _items2_1_2[x];
                            patturn3 = System.Convert.ToString(x + 1);
							_editing3 = false;
						}
					}
				}
				break;
			}
		}
		
		
		if(_select2 &&_selectedItem1 == "sth") {
			switch(_selectedItem2){
			case "hp" :
				if(_editing3){
					for(int x = 0; x < _items2_2_1.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/1.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_2_1[x])) {
                            _selectedItem3 = _items2_2_1[x];
                            patturn3 = System.Convert.ToString(x + 1);
							_editing3 = false;
						}
					}
					//
				}
				break;
			case "mp" :
				if(_editing3){
					for(int x = 0; x < _items2_2_2.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/1.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_2_2[x])) {
                            _selectedItem3 = _items2_2_2[x];
                            patturn3 = System.Convert.ToString(x + 1);
							_editing3 = false;
						}
					}
				}
				break;
			case "attackgauge" :
				if(_editing3){
					for(int x = 0; x < _items2_2_3.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/1.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_2_3[x])) {
                            _selectedItem3 = _items2_2_3[x];
                            patturn3 = System.Convert.ToString(x + 1);
							_editing3 = false;
						}
					}
					//
				}
				break;
			case "stet" :
				if(_editing3){
					for(int x = 0; x < _items2_2_4.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/1.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_2_4[x])) {
                            _selectedItem3 = _items2_2_4[x];
                            patturn3 = System.Convert.ToString(x + 1);
							_editing3 = false;
						}
					}
					//
				}
				break;
			case "buff" :
				if(_editing3){
					for(int x = 0; x < _items2_2_5.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/1.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_2_5[x])) {
                            _selectedItem3 = _items2_2_5[x];
                            patturn3 = System.Convert.ToString(x + 1);
							_editing3 = false;
						}
					}
					//
				}
				break;
			case "debuff" :
				if(_editing3){
					for(int x = 0; x < _items2_2_6.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/1.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_2_6[x])) {
                            _selectedItem3 = _items2_2_6[x];
                            patturn3 = System.Convert.ToString(x + 1);
							_editing3 = false;
						}
					}
					//
				}
				break;
			}
		}
		
		
		if(_select2 &&_selectedItem1 == "avg") {
			switch(_selectedItem2){
			case "hp" :
				if(_editing3){
                    Debug.LogError("z");
					for(int x = 0; x < _items2_3_1.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/1.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_3_1[x])) {
                            _selectedItem3 = _items2_3_1[x];
                            patturn3 = System.Convert.ToString(x + 1);
							_editing3 = false;
						}
					}
					//
				}
				break;
			case "mp" :
				if(_editing3){
					for(int x = 0; x < _items2_3_2.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/1.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_3_2[x])) {
                            _selectedItem3 = _items2_3_2[x];
                            patturn3 = System.Convert.ToString(x + 1);
							_editing3 = false;
						}
					}
					//
				}
				break;
			case "attackgauge" :
				if(_editing3){
					for(int x = 0; x < _items2_3_3.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/1.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_3_3[x])) {
                            _selectedItem3 = _items2_3_3[x];
                            patturn3 = System.Convert.ToString(x + 1);
							_editing3 = false;
						}
					}
					//
				}
				break;
			case "stet" :
				if(_editing3){
					for(int x = 0; x < _items2_3_4.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/1.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_3_4[x])) {
                            _selectedItem3 = _items2_3_4[x];
                            patturn3 = System.Convert.ToString(x + 1);
							_editing3 = false;
						}
					}
					//
				}
				break;
			case "buff" :
				if(_editing3){
					for(int x = 0; x < _items2_3_5.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/1.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_3_5[x])) {
                            _selectedItem3 = _items2_3_5[x];
                            patturn3 = System.Convert.ToString(x + 1);
							_editing3 = false;
						}
					}
				}
				break;
			case "debuff" :
				if(_editing3){
					for(int x = 0; x < _items2_3_6.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/1.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_3_6[x])) {
                            _selectedItem3 = _items2_3_6[x];
                            patturn3 = System.Convert.ToString(x + 1);
							_editing3 = false;
						}
					}
					//
				}
				break;
			}
		}
		
		if(_select2 &&_selectedItem1 == "self") {
			switch(_selectedItem2){
			case "hp" :
				if(_editing3){
					for(int x = 0; x < _items2_4_1.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/1.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_4_1[x])) {
                            _selectedItem3 = _items2_4_1[x];
                            patturn3 = System.Convert.ToString(x + 1);
							_editing3 = false;
						}
					}
					//
				}
				break;
			case "mp" :
				if(_editing3){
					for(int x = 0; x < _items2_4_2.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/1.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_4_2[x])) {
                            _selectedItem3 = _items2_4_2[x];
                            patturn3 = System.Convert.ToString(x + 1);
							_editing3 = false;
						}
					}
					//
				}
				break;
			case "attackgauge" :
				if(_editing3){
					for(int x = 0; x < _items2_4_3.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/1.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_4_3[x])) {
                            _selectedItem3 = _items2_4_3[x];
                            patturn3 = System.Convert.ToString(x + 1);
							_editing3 = false;
						}
					}
					//
				}
				break;
			case "stet" :
				if(_editing3){
					for(int x = 0; x < _items2_4_4.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/1.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_4_4[x])) {
                            _selectedItem3 = _items2_4_4[x];
                            patturn3 = System.Convert.ToString(x + 1);
							_editing3 = false;
						}
					}
					//
				}
				break;
			case "buff" :
				if(_editing3){
					for(int x = 0; x < _items2_4_5.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/1.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_4_5[x])) {
                            _selectedItem3 = _items2_4_5[x];
                            patturn3 = System.Convert.ToString(x + 1);
							_editing3 = false;
						}
					}
					//
				}
				break;
			case "debuff" :
				if(_editing3){
					for(int x = 0; x < _items2_4_6.Length; x++) {
						if(GUI.Button (new Rect(_screenWidth/1.5f, (20 * x) + _screenHeight/3.2f + 20, 80, 20), _items2_4_6[x])) {
                            _selectedItem3 = _items2_4_6[x];
                            patturn3 = System.Convert.ToString(x + 1);
							_editing3 = false;
						}
					}
					//
				}
				break;
			}
		}

		if(_editing4){
			_editing4=false;
			XmlDocument XmlDoc = new XmlDocument();
			XmlDoc.Load (".\\Assets\\Resources\\Data\\Players\\Xml\\0000000.xml");
			XmlNode aaa = XmlDoc.DocumentElement;	
			XmlNodeList ccc = aaa.ChildNodes;
			Debug.LogError(patturn1);
			Debug.LogError(patturn2);
			Debug.LogError(patturn3);

			foreach (XmlNode XN in ccc) {
				if(XN.LocalName.Equals("patturn1")){
					XN.InnerXml = patturn1;
				}
				if(XN.LocalName.Equals("patturn2")){
					XN.InnerXml = patturn2;
                }
				if(XN.LocalName.Equals("patturn3")){
					XN.InnerXml = patturn3;
                }
			}


			XmlDoc.Save(".\\Assets\\Resources\\Data\\Players\\Xml\\0000000111.xml");

		}

	}
}
