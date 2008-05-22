################################################################################
# Path
################################################################################
MONO          = C:/progra~1/Mono-1.9.1/bin/
OUTPUT        = ./
OUTPUT_SERVER = $(OUTPUT)BdtServer/
OUTPUT_CLIENT = $(OUTPUT)BdtClient/
OUTPUT_CLIGUI = $(OUTPUT)BdtGuiClient/
OUTPUT_WEBSRV = $(OUTPUT)BdtWebServer/
OUTPUT_WEBBIN = $(OUTPUT)BdtWebServer/bin/

################################################################################
# Tools
################################################################################
CC     = $(MONO)gmcs
RESGEN = $(MONO)resgen
AL     = $(MONO)al
CFLAGS = -debug- -optimize+ -warn:4

################################################################################
# Source files
################################################################################
SHARED_FILES = $(wildcard ../BdtShared/Properties/*.cs ../BdtShared/Configuration/*.cs ../BdtShared/Console/*.cs ../BdtShared/Logs/*.cs ../BdtShared/Protocol/*.cs ../BdtShared/Request/*.cs ../BdtShared/Response/*.cs ../BdtShared/Service/*.cs ../BdtShared/Runtime/*.cs ../BdtShared/Resources/*.cs)
SERVER_FILES = $(wildcard ../BdtServer/Properties/*.cs ../BdtServer/Runtime/*.cs ../BdtServer/Service/*.cs ../BdtServer/Resources/*.cs)
CLIENT_FILES = $(wildcard ../BdtClient/Properties/*.cs ../BdtClient/Configuration/*.cs ../BdtClient/Service/*.cs ../BdtClient/Runtime/*.cs ../BdtClient/Socks/*.cs ../BdtClient/Sockets/*.cs ../BdtClient/Resources/*.cs)
WEBSRV_FILES = $(wildcard ../BdtWebServer/Properties/*.cs ../BdtWebServer/*.cs ../BdtWebServer/Runtime/*.cs)


################################################################################
# Culture resources
################################################################################
CULTURE_FR      = fr
FCULTURE_IN     = ../Bdt$(call FCULTURE_TRG,${1})/Resources/Strings${2}.resx
FCULTURE_CNV    = $(call FCULTURE_IN,${1},${2}),$(call FCULTURE_OUT,${1},$(2))
FCULTURE_OUT    = $(OUTPUT)Bdt.$(call FCULTURE_TRG,${1}).Resources.Strings${2}.resources
FCULTURE_TRG    = $(subst Bdt,,$(notdir $(basename ${1})))
FCULTURE_FR_SAT = $(CULTURE_FR)/$(notdir $(basename ${1})).resources.dll

################################################################################
# Rules
################################################################################
all:					BdtClient BdtServer BdtWebServer
BdtClient:		$(OUTPUT_CLIENT)BdtShared.dll $(OUTPUT_CLIENT)BdtClient.exe $(OUTPUT_CLIENT)BdtClientCfg.xml
BdtServer:		$(OUTPUT_SERVER)BdtShared.dll $(OUTPUT_SERVER)BdtServer.exe $(OUTPUT_SERVER)BdtServerCfg.xml
BdtWebServer:	$(OUTPUT_WEBBIN)BdtShared.dll $(OUTPUT_WEBBIN)BdtServer.dll $(OUTPUT_WEBBIN)BdtWebServer.dll $(OUTPUT_WEBSRV)BdtServerCfg.xml $(OUTPUT_WEBSRV)Global.asax $(OUTPUT_WEBSRV)Web.config

################################################################################
# Client build
################################################################################
$(OUTPUT_CLIENT)BdtShared.dll: $(SHARED_FILES)
		mkdir -p $(OUTPUT_CLIENT)
		mkdir -p $(OUTPUT_CLIENT)$(CULTURE_FR)
		$(RESGEN) /compile $(call FCULTURE_CNV,$@)
		$(CC) $(CFLAGS) /reference:System.Runtime.Remoting.dll /reference:System.Xml.dll /out:$@ /target:library /resource:$(call FCULTURE_OUT,$@) $(SHARED_FILES)
		$(RESGEN) /compile $(call FCULTURE_CNV,$@,.$(CULTURE_FR))
		$(AL) /t:lib /embed:$(call FCULTURE_OUT,$@,.$(CULTURE_FR)) /culture:$(CULTURE_FR) /out:$(OUTPUT_CLIENT)$(call FCULTURE_FR_SAT,$@)
		
$(OUTPUT_CLIENT)BdtClient.exe: $(CLIENT_FILES)
		$(RESGEN) /compile $(call FCULTURE_CNV,$@)
		$(CC) $(CFLAGS) /reference:$(OUTPUT_CLIENT)BdtShared.dll /out:$@ /target:exe /resource:$(call FCULTURE_OUT,$@) $(CLIENT_FILES)
		$(RESGEN) /compile $(call FCULTURE_CNV,$@,.$(CULTURE_FR))
		$(AL) /t:lib /embed:$(call FCULTURE_OUT,$@,.$(CULTURE_FR)) /culture:$(CULTURE_FR) /out:$(OUTPUT_CLIENT)$(call FCULTURE_FR_SAT,$@)
		@-rm *.resources
																	
$(OUTPUT_CLIENT)BdtClientCfg.xml: BdtClientCfg.xml
		cp -f BdtClientCfg.xml $@ 

################################################################################
# Server build
################################################################################
$(OUTPUT_SERVER)BdtShared.dll: $(SHARED_FILES)
		mkdir -p $(OUTPUT_SERVER)
		mkdir -p $(OUTPUT_SERVER)$(CULTURE_FR)
		$(RESGEN) /compile $(call FCULTURE_CNV,$@)
		$(CC) $(CFLAGS) /reference:System.Runtime.Remoting.dll /reference:System.Xml.dll /out:$@ /target:library /resource:$(call FCULTURE_OUT,$@) $(SHARED_FILES)
		$(RESGEN) /compile $(call FCULTURE_CNV,$@,.$(CULTURE_FR))
		$(AL) /t:lib /embed:$(call FCULTURE_OUT,$@,.$(CULTURE_FR)) /culture:$(CULTURE_FR) /out:$(OUTPUT_SERVER)$(call FCULTURE_FR_SAT,$@)
																	
$(OUTPUT_SERVER)BdtServer.exe: $(SERVER_FILES)
		$(RESGEN) /compile $(call FCULTURE_CNV,$@)
		$(CC) $(CFLAGS) /reference:$(OUTPUT_SERVER)BdtShared.dll /out:$@ /target:exe /resource:$(call FCULTURE_OUT,$@) $(SERVER_FILES)
		$(RESGEN) /compile $(call FCULTURE_CNV,$@,.$(CULTURE_FR))
		$(AL) /t:lib /embed:$(call FCULTURE_OUT,$@,.$(CULTURE_FR)) /culture:$(CULTURE_FR) /out:$(OUTPUT_SERVER)$(call FCULTURE_FR_SAT,$@)
		@-rm *.resources
								  								
$(OUTPUT_SERVER)BdtServerCfg.xml: BdtServerCfg.xml
		cp -f BdtServerCfg.xml $@ 
												
################################################################################
# Web Server build
################################################################################
$(OUTPUT_WEBBIN)BdtShared.dll:
		mkdir -p $(OUTPUT_WEBSRV)
		mkdir -p $(OUTPUT_WEBBIN)
		mkdir -p $(OUTPUT_WEBBIN)$(CULTURE_FR)
		$(RESGEN) /compile $(call FCULTURE_CNV,$@)
		$(CC) $(CFLAGS) /reference:System.Runtime.Remoting.dll /reference:System.Xml.dll /out:$@ /target:library /resource:$(call FCULTURE_OUT,$@) $(SHARED_FILES)
		$(RESGEN) /compile $(call FCULTURE_CNV,$@,.$(CULTURE_FR))
		$(AL) /t:lib /embed:$(call FCULTURE_OUT,$@,.$(CULTURE_FR)) /culture:$(CULTURE_FR) /out:$(OUTPUT_WEBBIN)$(call FCULTURE_FR_SAT,$@)


$(OUTPUT_WEBBIN)BdtServer.dll:
		$(RESGEN) /compile $(call FCULTURE_CNV,$@)
		$(CC) $(CFLAGS) /reference:$(OUTPUT_WEBBIN)BdtShared.dll /out:$@ /target:library /resource:$(call FCULTURE_OUT,$@) $(SERVER_FILES)
		$(RESGEN) /compile $(call FCULTURE_CNV,$@,.$(CULTURE_FR))
		$(AL) /t:lib /embed:$(call FCULTURE_OUT,$@,.$(CULTURE_FR)) /culture:$(CULTURE_FR) /out:$(OUTPUT_WEBBIN)$(call FCULTURE_FR_SAT,$@)
		@-rm *.resources

$(OUTPUT_WEBBIN)BdtWebServer.dll:
		$(CC) $(CFLAGS) /reference:System.Web.dll /reference:$(OUTPUT_WEBBIN)BdtShared.dll /reference:$(OUTPUT_WEBBIN)BdtServer.dll /out:$@ /target:library $(WEBSRV_FILES)

$(OUTPUT_WEBSRV)BdtServerCfg.xml:
		cp -f BdtServerCfg.xml $@ 

$(OUTPUT_WEBSRV)Global.asax:
		cp -f ../BdtWebServer/Global.asax $@

$(OUTPUT_WEBSRV)Web.config:
		cp -f ../BdtWebServer/Web.config $@												
																	
################################################################################
# Cleaning
################################################################################
clean:
		@-rm -fr *.resources
		@-rm -fr $(OUTPUT_SERVER)
		@-rm -fr $(OUTPUT_CLIENT)
		@-rm -fr $(OUTPUT_CLIGUI)
		@-rm -fr $(OUTPUT_WEBSRV)
		@-rm -fr ..\\BdtShared\\bin
		@-rm -fr ..\\BdtShared\\obj
		@-rm -fr ..\\BdtServer\\bin
		@-rm -fr ..\\BdtServer\\obj
		@-rm -fr ..\\BdtClient\\bin
		@-rm -fr ..\\BdtClient\\obj
		@-rm -fr ..\\BdtGuiClient\\bin
		@-rm -fr ..\\BdtGuiClient\\obj
		@-rm -fr ..\\BdtWebServer\\bin
		@-rm -fr ..\\BdtWebServer\\obj
								