<?php
/**
 * CakePHP(tm) : Rapid Development Framework (https://cakephp.org)
 * Copyright (c) Cake Software Foundation, Inc. (https://cakefoundation.org)
 *
 * Licensed under The MIT License
 * For full copyright and license information, please see the LICENSE.txt
 * Redistributions of files must retain the above copyright notice.
 *
 * @copyright     Copyright (c) Cake Software Foundation, Inc. (https://cakefoundation.org)
 * @link          https://cakephp.org CakePHP(tm) Project
 * @since         3.2.0
 * @license       https://opensource.org/licenses/mit-license.php MIT License
 */
namespace Cake\Database;

use Cake\Database\Type;
use Cake\Database\Type\BatchCastingInterface;
use Cake\Database\Type\OptionalConvertInterface;

/**
 * A callable class to be used for processing each of the rows in a statement
 * result, so that the values are converted to the right PHP types.
 */
class FieldTypeConverter
{
    /**
     * An array containing the name of the fields and the Type objects
     * each should use when converting them.
     *
     * @var array
     */
    protected $_typeMap;

    /**
     * An array containing the name of the fields and the Type objects
     * each should use when converting them using batching.
     *
     * @var array
     */
    protected $batchingTypeMap;

    /**
     * An array containing all the types registered in the Type system
     * at the moment this object is created. Used so that the types list
     * is not fetched on each single row of the results.
     *
     * @var array
     */
    protected $types;

    /**
     * The driver object to be used in the type conversion
     *
     * @var \Cake\Database\Driver
     */
    protected $_driver;

    /**
     * Builds the type map
     *
     * @param \Cake\Database\TypeMap $typeMap Contains the types to use for converting results
     * @param \Cake\Database\Driver $driver The driver to use for the type conversion
     */
    public function __construct(TypeMap $typeMap, Driver $driver)
    {
        $this->_driver = $driver;
        $map = $typeMap->toArray();
        $types = Type::buildAll();

        $simpleMap = $batchingMap = [];
        $simpleResult = $batchingResult = [];

        foreach ($types as $k => $type) {
            if ($type instanceof OptionalConvertInterface && !$type->requiresToPhpCast()) {
                continue;
            }

            // Because of backwards compatibility reasons, we won't allow classes
            // inheriting Type in userland code to be batchable, even if they implement
            // the interface. Users can implement the TypeInterface instead to have
            // access to this feature.
            $batchingType = $type instanceof BatchCastingInterface &&
                !($type instanceof Type &&
                strpos(get_class($type), 'Cake\Database\Type') === false);

            if ($batchingType) {
                $batchingMap[$k] = $type;
                continue;
            }

            $simpleMap[$k] = $type;
        }

        foreach ($map as $field => $type) {
            if (isset($simpleMap[$type])) {
                $simpleResult[$field] = $simpleMap[$type];
                continue;
            }
            if (isset($batchingMap[$type])) {
                $batchingResult[$type][] = $field;
            }
        }

        // Using batching when there is only a couple for the type is actually slower,
        // so, let's check for that case here.
        foreach ($batchingResult as $type => $fields) {
            if (count($fields) > 2) {
                continue;
            }

            foreach ($fields as $f) {
                $simpleResult[$f] = $batchingMap[$type];
            }
            unset($batchingResult[$type]);
        }

        $this->types = $types;
        $this->_typeMap = $simpleResult;
        $this->batchingTypeMap = $batchingResult;
    }

    /**
     * Converts each of the fields in the array that are present in the type map
     * using the corresponding Type class.
     *
     * @param array $row The array with the fields to be casted
     * @return array
     */
    public function __invoke($row)
    {
        if (!empty($this->_typeMap)) {
            foreach ($this->_typeMap as $field => $type) {
                $row[$field] = $type->toPHP($row[$field], $this->_driver);
            }
        }

        if (!empty($this->batchingTypeMap)) {
            foreach ($this->batchingTypeMap as $t => $fields) {
                $row = $this->types[$t]->manyToPHP($row, $fields, $this->_driver);
            }
        }

        return $row;
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       (   \         (   `  
       (   j         (   n         (   (         '      
                                       	   	                                     	   #   
   $                  
                                       
      	                        	   !                        %      '   	   "      (   
   &      p    %?N? ???x\???
? ? ?5?O??/??
?
ru	?	a?oA?t?o?                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   `?A<SymbolTreeInfo>_SpellChecker_D:\KIA\AppInPhieu\AppInPhieu v3\AppInPhieu\AppInPhieu.csproj?r?g<SymbolTreeInfo>_SpellChecker_D:\KIA\AppInPhieu\AppInPhieu v3\AppInPhieu\bin\Debug\DocumentFormat.OpenXml.dll(??<SymbolTreeInfo>_SpellChecker_C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\mscorlib.dll'? ?<SymbolTreeInfo>_SpellChecker_C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.dll&??<SymbolTreeInfo>_SpellChecker_C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.Windows.Forms.dll%n?_<SymbolTreeInfo>_Metadata_D:\KIA\AppInPhieu\AppInPhieu v3\AppInPhieu\bin\Debug\DocumentFormat.OpenXml.dll$l?[<SymbolTreeInfo>_SpellChecker_C:\WINDOWS\assembly\GAC_MSIL\Office\15.0.0.0__71e9bce111e9429c\Office.dll#? Z?5<SymbolTreeInfo>_Source_D:\KIA\AppInPhieu\AppInPhieu v3\AppInPhieu\AppInPhieu.csproj@??;<SymbolTreeInfo>_SpellChecker_C:\WINDOWS\assembly\GAC_MSIL\Microsoft.Office.Interop.Excel\15.0.0.0__71e9bce111e9429c\Microsoft.Office.Interop.Excel.dll!??<SymbolTreeInfo>_SpellChecker_C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.Configuration.dll e?M<SymbolTreeInfo>_SpellChecker_D:\KIA\AppInPhieu\AppInPhieu v3\AppInPhieu\bin\Debug\ClosedXML.dll?
?<SymbolTreeInfo>_SpellChecker_C:\WINDOWS\assembly\GAC_MSIL\Microsoft.Vbe.Interop\15.0.0.0__71e9bce111e9429c\Microsoft.Vbe.Interop.dll??<SymbolTreeInfo>_SpellChecker_C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.Xml.dll?	?<SymbolTreeInfo>_SpellChecker_C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.Xml.Linq.dll?	?<SymbolTreeInfo>_SpellChecker_C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.Net.Http.dll??<SymbolTreeInfo>_SpellChecker_C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.Drawing.dll??'<SymbolTreeInfo>_SpellChecker_C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.DirectoryServices.dll??<SymbolTreeInfo>_SpellChecker_C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.Deployment.dll??<SymbolTreeInfo>_SpellChecker_C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.Data.dll??1<SymbolTreeInfo>_SpellChecker_C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.Data.DataSetExtensions.dll??<SymbolTreeInfo>_SpellChecker_C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.Core.dll?
?<SymbolTreeInfo>_SpellChecker_C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\Microsoft.CSharp.dll   Z<SymbolTreeInfo>_Source_D:\KIA\AppInPhieu\AppInPhieu v3\AppInPhieu\AppInPhieu.csproja?E<SymbolTreeInfo>_Metadata_D:\KIA\AppInPhieu\AppInPhieu v3\AppInPhieu\bin\Debug\ClosedXML.dllh?S<SymbolTreeInfo>_Metadata_C:\WINDOWS\assembly\GAC_MSIL\Office\15.0.0.0__71e9bce111e9429c\Office.dll	       %?N? ????K??8?    ? ? ?                                                                                                                               ?g??d?????a ?L?	12p?????_??6????E]2    ?  ???n??,L?z??&{??l?A?CX?`??9etW??awK?a !??E?a?t?:??????++;?y?B)?X??W~?"??\:}??!???YI???"i????Lc?"?Z4T??:k??L???p???t???=n??Y????1??%>?-?? ?&J?!??S?K?!q'N?$0f?a?xCI?/3?2????P u^N?t??s,N?u??+??}x?6????k%jM???u?mGh??pb????|?PR??? ??q??????,5A????77h??=?2??;6B?b?V???9????j)??R?@?2????p?+?=/??P	?WO?14?+? %??8PGp$?????LY?;???m?gc?+??4??A(?Gn?	???Cl?)|E?Svl?SIl?q}^??_l
X??J(??:?b?K??k?42/pCl?1??????x?????n?yX??%?B???%a??[e_???   ?  ?????t^GN??)???y
%??E???w::??X?1_#?`M?????_????$????\wp??Iz????e?Z???????%???bF???d%?'uV>???????$U??e???i??<?.F^S????E#?=?a????I????'?A,j?tOY?$??(:???!??>?=?6???M*??f??%??????(???D?8???|P@d??T???4?L??????T????0??????C???w<?_???P?'sm???S?S?c+?.??k^??T?Tt?Dm?US2?K?=???7??jX????8lH7?,<??'?T???????Y *9?????
?N??l s??	??C??????\??????,,x?[??o%?r??B?????s??W??}????Y???(???<?F??? ?q ???B??????bt_o????D????CnGw??5?????sc?!AS#??1??nyc?N?*,??F?,???L8ewok$I?@?:?`?#??'??gls??#??B<?S&?ih+vs??E??Fb	,??9$ ????y??V??????`D?:??-???8t|?8Ed?}???f5?!?Qn,M?
?R???^?Al?????gL?:?X??h+Q ????????]?d??W7:??&$i????|???q?       ? ?0B E   4   Form1 
AppInPhieu`   ?        Formcon AppInPhieu.Form1   ?         mayin    V         sophieudain    ?         CountSP             TThai             MM             colorcar    -         CNCL    <         TLBT    B         TLTB    H         TT    N         SN    R         NC    V         STK    Z         FullVin    _         ArEr    h         dayin    w         ()b   ?         
Form1_Load(object, EventArgs)  ?  
       Init_listview   ?         Init_listview2   ?         checkeng   u         checkSTK   ?         ShowSTK(string)  ?         
Show_List2   ?#  
       	Show_List   ?)  	       	CheckInfo   F0  	       
CheckInfo2   {8  
        lst_inphieu_SelectedIndexChanged  ?@          txt_search_TextChanged  UB         toolStripTextBox1_Click  sN         $ti???ngVi???tToolStripMenuItem_Click  ?N          englishToolStripMenuItem_Click  ?O         Up_ar(string, string)  WP         dtp_time_ValueChanged  3S         cbb_soloai_SelectedIndexChanged  ?S         cbb_mamau_SelectedIndexChanged  '^         ExportReportDay   ?i         Form1_FormClosing(object, FormClosingEventArgs)  ??         CheckSP   4?         ConvertColor  ??         Ins_P   ?         
checkPhieu  ??  
       txt_vin_TextChanged  |?         CheckModelMorning  ??         checkVIN  ?         CheckAr   ??         addcombomodel   ??         exportexcel   3?         writeLog  Q?         btn_Ip_Click  ?         ?~?????a ? ?	12???A	?9?R
?@L`U]??S2    "   0'??NP?f??j	?=???????3g????C   h   ?l	N????{??? ^?? ?e-~2!? ???
3R?D&0yZH??s"?06???6p??????&??k?S????V???,?????Mg??????,???h?{                    	Resources AppInPhieu.Properties@   y  	       resourceMan AppInPhieu.Properties.Resources   ?         resourceCulture    (         ()B   ?  	       ResourceManager M   ?         Culture M   g
         _2 M   ?         _xe_mazda_cx_5_2018_2 M   4         )a001_c003_20180330_r_00000_01688_still003 M   ?  )       close M   ?         CX525 M   ?         KIA1 M   l         KIA2 M   ?         KIA3 M   P         tick M   ?           $  $%?N? ??I?[r\i?   ? 0?                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      ?*?@?5?.<SymbolTreeInfo>_Source_D:\KIA\AppInPhieu\AppInPhieu v3\AppInPhieu\AppInPhieu.csproj?	17rjC?)??v?????@+?@o???AppInPhieuUploaddataDisposefrm_ColorDisposeManagerDisposePropertiesResourcesResourceManagerCulture_2_xe_mazda_cx_5_2018_2a001_c003_20180330_r_00000_01688_still003closeCX525KIA1KIA2KIA3tickSettingsDefaultProgramForm1DisposeForm2Disposetab_baocaotab_managerfrm_setupDisposefrm_DetailDisposeReportDisposefrm_MainchkSetParameterValueDelegateInvokeBeginInvokeEndInvokecheckexitformDisposetabControl1lbl_userfr_loginDisposeUsernameuserfrm_InfoDispose4           ????b      *   d      *   y   )   *       
       W     +   k        5        ?      *   [      *   ?      *   ?      ,   ?         ?         ?        $                 ?        x                2      %   &     (         1   b  	   +   ?         ?         ?           	        
      ?        -        ?   	      Q     +   ?      *   ?      *   ?      *   ?        +         ?         9   
               L      *   C   	   '   8        ?      '   ?   
      ?                 ?      *   
   
      ?     3   ?            ?L???A?f<SymbolTreeInfo>_SpellChecker_D:\KIA\AppInPhieu\AppInPhieu v3\AppInPhieu\AppInPhieu.csproj?	3rjC?)??v?????@+?@o???  _ 2 _ x e _ m a z d a _ c x _ 5 _ 2 0 1 8 _ 2 a 0 0 1 _ c 0 0 3 _ 2 0 1 8 0 3 3 0 _ r _ 0 0 0 0 0 _ 0 1 6 8 8 _ s t i l l 0 0 3 a p p i n p h i e u b e g i n i n v o k e c h e c k e x i t f o r m c h k c l o s e c u l t u r e c x 5 2 5 d e f a u l t d i s p o 